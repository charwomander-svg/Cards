using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class SpiderRules : ICardGameRules
{
    public string Name { get; }
    public int MinPlayers => 1;
    public int MaxPlayers => 1;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, false, true, true, false, false, false, false, true, false, new[] { "deal-row", "move-sequence" });

    public SpiderRules(string name = "Spider")
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
    }

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Spider is a one-player game.");

        var session = new CardGameSession(Name, playerCount);
        var decks = new List<Card>(104);
        for (int i = 0; i < 2; i++)
            decks.AddRange(new Deck().Cards);
        var rng = random ?? Random.Shared;
        for (int i = decks.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (decks[i], decks[j]) = (decks[j], decks[i]);
        }

        session.AddPile("stock");
        for (int i = 1; i <= 8; i++)
            session.AddPile($"foundation{i}");
        for (int i = 1; i <= 10; i++)
            session.AddPile($"tableau{i}");

        int index = 0;
        for (int column = 1; column <= 10; column++)
        {
            CardPile tableau = session.GetPile($"tableau{column}");
            int count = column <= 4 ? 6 : 5;
            for (int card = 0; card < count; card++)
                tableau.Add(decks[index++], faceUp: card == count - 1);
        }

        while (index < decks.Count)
            session.GetPile("stock").Add(decks[index++], faceUp: false);

        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var moves = new List<MoveDescriptor>();
        if (session.GetPile("stock").Count >= 10)
            moves.Add(new MoveDescriptor("deal-row", "stock", "tableau", 10));

        foreach (CardPile source in session.Piles.Values.Where(p => IsTableau(p) && !p.IsEmpty && p.IsFaceUp(p.Top)))
        {
            IReadOnlyList<Card> sequence = GetInSuitSequence(source);
            for (int count = 1; count <= sequence.Count; count++)
            {
                Card first = sequence[^count];
                foreach (CardPile destination in session.Piles.Values.Where(p => IsTableau(p) && p.Name != source.Name))
                    if (CanMoveToTableau(first, destination))
                        moves.Add(new MoveDescriptor("move-sequence", source.Name, destination.Name, count, Cards: sequence.Skip(sequence.Count - count).Select(c => c.ToString()).ToArray()));
            }
        }

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        if (move.Type.Equals("deal-row", StringComparison.OrdinalIgnoreCase))
            return DealRow(session);

        CardPile source = session.GetPile(move.Source);
        CardPile destination = session.GetPile(move.Destination ?? string.Empty);
        IReadOnlyList<Card> sequence = GetInSuitSequence(source);
        if (move.Count < 1 || move.Count > sequence.Count)
            return MoveResult.Failure("Requested sequence is not movable.");

        Card first = sequence[^move.Count];
        if (!CanMoveToTableau(first, destination))
            return MoveResult.Failure("Sequence cannot be moved there.");

        IReadOnlyList<Card> cards = source.DrawSequence(move.Count);
        foreach (Card card in cards)
            destination.Add(card);
        FlipTop(source);
        session.Statistics.RecordPileRemoved(source.Name, move.Count);
        session.RecordPlay(0, destination.Name, move.Count);
        ClearCompletedRuns(session, destination);
        return MoveResult.Success($"Moved {move.Count} card(s).");
    }

    private static MoveResult DealRow(CardGameSession session)
    {
        CardPile stock = session.GetPile("stock");
        if (stock.Count < 10)
            return MoveResult.Failure("Not enough stock cards to deal a row.");
        if (session.Piles.Values.Any(p => IsTableau(p) && p.IsEmpty))
            return MoveResult.Failure("Cannot deal a Spider row while a tableau is empty.");

        for (int i = 1; i <= 10; i++)
            session.GetPile($"tableau{i}").Add(stock.Draw());
        session.RecordDraw(0, "stock", 10);
        return MoveResult.Success("Dealt one card to each tableau.");
    }

    private static void ClearCompletedRuns(CardGameSession session, CardPile tableau)
    {
        IReadOnlyList<Card> sequence = GetInSuitSequence(tableau);
        if (sequence.Count < 13 || sequence[0].Rank != Rank.King || sequence[^1].Rank != Rank.Ace)
            return;

        IReadOnlyList<Card> run = tableau.DrawSequence(13);
        CardPile foundation = session.Piles.Values.First(p => p.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase) && p.IsEmpty);
        foreach (Card card in run)
            foundation.Add(card);
        session.RecordClear(0, foundation.Name, 13);
        session.IsComplete = session.Piles.Values.Where(p => p.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase)).Sum(p => p.Count) == 104;
    }

    private static IReadOnlyList<Card> GetInSuitSequence(CardPile pile)
    {
        var sequence = new List<Card>();
        for (int i = pile.Cards.Count - 1; i >= 0; i--)
        {
            Card card = pile.Cards[i];
            if (!pile.IsFaceUp(card))
                break;
            if (sequence.Count > 0)
            {
                Card previous = sequence[^1];
                if (previous.Suit != card.Suit || !RuleHelpers.IsOneRankLower(previous, card))
                    break;
            }
            sequence.Add(card);
        }
        sequence.Reverse();
        return sequence;
    }

    private static bool CanMoveToTableau(Card card, CardPile tableau) => tableau.IsEmpty || RuleHelpers.IsOneRankLower(card, tableau.Top);
    private static bool IsTableau(CardPile pile) => pile.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase);
    private static void FlipTop(CardPile pile)
    {
        if (!pile.IsEmpty && !pile.IsFaceUp(pile.Top))
            pile.FlipTop();
    }
}
