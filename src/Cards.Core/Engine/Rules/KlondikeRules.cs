using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class KlondikeRules : ICardGameRules
{
    private const string Stock = "stock";
    private const string Waste = "waste";
    private const string FoundationPrefix = "foundation";
    private const string TableauPrefix = "tableau";

    public string Name => "Klondike";
    public int MinPlayers => 1;
    public int MaxPlayers => 1;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, false, true, true, false, false, false, false, true, false, new[] { "draw", "move" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Klondike is a one-player game.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);

        session.AddPile(Stock);
        session.AddPile(Waste);
        for (int i = 1; i <= 4; i++)
            session.AddPile($"{FoundationPrefix}{i}");
        for (int i = 1; i <= 7; i++)
            session.AddPile($"{TableauPrefix}{i}");

        for (int column = 1; column <= 7; column++)
        {
            CardPile tableau = session.GetPile($"{TableauPrefix}{column}");
            for (int card = 0; card < column; card++)
                tableau.Add(deck.Draw(), faceUp: card == column - 1);
        }

        session.GetPile(Stock).AddRange(deck.Cards);
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var moves = new List<MoveDescriptor>();

        if (!session.GetPile(Stock).IsEmpty)
            moves.Add(new MoveDescriptor("draw", Stock, Waste));
        else if (!session.GetPile(Waste).IsEmpty)
            moves.Add(new MoveDescriptor("redeal", Waste, Stock));

        foreach (CardPile source in session.Piles.Values.Where(IsPlayableSource))
        {
            foreach (CardPile destination in session.Piles.Values.Where(p => p.Name.StartsWith(FoundationPrefix, StringComparison.OrdinalIgnoreCase)))
                if (CanMoveToFoundation(source.Top, destination))
                    moves.Add(new MoveDescriptor("move", source.Name, destination.Name, Cards: new[] { source.Top.ToString() }));

            foreach (CardPile destination in session.Piles.Values.Where(p => p.Name.StartsWith(TableauPrefix, StringComparison.OrdinalIgnoreCase) && p.Name != source.Name))
                if (CanMoveToTableau(source.Top, destination))
                    moves.Add(new MoveDescriptor("move", source.Name, destination.Name, Cards: new[] { source.Top.ToString() }));

            if (source.Name.StartsWith(TableauPrefix, StringComparison.OrdinalIgnoreCase))
            {
                IReadOnlyList<Card> movable = GetFaceUpSequence(source);
                for (int count = 2; count <= movable.Count; count++)
                {
                    Card first = movable[^count];
                    foreach (CardPile destination in session.Piles.Values.Where(p => p.Name.StartsWith(TableauPrefix, StringComparison.OrdinalIgnoreCase) && p.Name != source.Name))
                        if (CanMoveToTableau(first, destination))
                            moves.Add(new MoveDescriptor("move-sequence", source.Name, destination.Name, count, Cards: movable.Skip(movable.Count - count).Select(c => c.ToString()).ToArray()));
                }
            }
        }

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
        {
            CardPile stock = session.GetPile(Stock);
            CardPile waste = session.GetPile(Waste);
            if (stock.IsEmpty)
                return MoveResult.Failure("The stock is empty.");

            waste.Add(stock.Draw());
            session.RecordDraw(0, Stock, 1);
            session.Statistics.RecordPileAdded(Waste, 1, waste.Count);
            return MoveResult.Success("Drew a card to the waste.");
        }

        if (move.Type.Equals("redeal", StringComparison.OrdinalIgnoreCase))
            return RedealWaste(session);

        CardPile source = session.GetPile(move.Source);
        CardPile destination = session.GetPile(move.Destination ?? string.Empty);
        if (source.IsEmpty)
            return MoveResult.Failure("Source pile is empty.");

        if (move.Type.Equals("move-sequence", StringComparison.OrdinalIgnoreCase))
            return MoveSequence(session, source, destination, move.Count);

        Card card = source.Top;
        if (!source.IsFaceUp(card))
            return MoveResult.Failure("Cannot move a face-down card.");
        bool legal = destination.Name.StartsWith(FoundationPrefix, StringComparison.OrdinalIgnoreCase)
            ? CanMoveToFoundation(card, destination)
            : CanMoveToTableau(card, destination);

        if (!legal)
            return MoveResult.Failure("That card cannot be moved to the destination pile.");

        destination.Add(source.Draw());
        FlipNewTableauTop(source);
        session.Statistics.RecordPileRemoved(source.Name, 1);
        if (destination.Name.StartsWith(FoundationPrefix, StringComparison.OrdinalIgnoreCase))
            session.RecordClear(0, destination.Name, 1);
        else
            session.RecordPlay(0, destination.Name, 1);
        session.IsComplete = session.Piles.Values
            .Where(p => p.Name.StartsWith(FoundationPrefix, StringComparison.OrdinalIgnoreCase))
            .Sum(p => p.Count) == 52;
        return MoveResult.Success();
    }

    private static MoveResult RedealWaste(CardGameSession session)
    {
        CardPile waste = session.GetPile(Waste);
        CardPile stock = session.GetPile(Stock);
        if (waste.IsEmpty)
            return MoveResult.Failure("Waste is empty.");

        while (!waste.IsEmpty)
            stock.Add(waste.Draw(), faceUp: false);

        return MoveResult.Success("Recycled the waste into the stock.");
    }

    private static MoveResult MoveSequence(CardGameSession session, CardPile source, CardPile destination, int count)
    {
        IReadOnlyList<Card> movable = GetFaceUpSequence(source);
        if (count < 1 || count > movable.Count)
            return MoveResult.Failure("Requested sequence is not movable.");

        Card first = movable[^count];
        if (!CanMoveToTableau(first, destination))
            return MoveResult.Failure("Sequence cannot be moved to the destination tableau.");

        IReadOnlyList<Card> sequence = source.DrawSequence(count);
        foreach (Card card in sequence)
            destination.Add(card);
        FlipNewTableauTop(source);
        session.Statistics.RecordPileRemoved(source.Name, count);
        session.RecordPlay(0, destination.Name, count);
        return MoveResult.Success($"Moved {count} cards.");
    }

    private static IReadOnlyList<Card> GetFaceUpSequence(CardPile pile)
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
                if (!RuleHelpers.IsOppositeColor(previous, card) || !RuleHelpers.IsOneRankLower(previous, card))
                    break;
            }
            sequence.Add(card);
        }

        sequence.Reverse();
        return sequence;
    }

    private static void FlipNewTableauTop(CardPile pile)
    {
        if (!pile.IsEmpty && pile.Name.StartsWith(TableauPrefix, StringComparison.OrdinalIgnoreCase) && !pile.IsFaceUp(pile.Top))
            pile.FlipTop();
    }

    private static bool CanMoveToFoundation(Card card, CardPile foundation)
    {
        if (foundation.IsEmpty)
            return card.Rank == Rank.Ace;

        Card top = foundation.Top;
        return card.Suit == top.Suit && RuleHelpers.IsOneRankHigher(card, top);
    }

    private static bool CanMoveToTableau(Card card, CardPile tableau)
    {
        if (tableau.IsEmpty)
            return card.Rank == Rank.King;

        Card top = tableau.Top;
        return RuleHelpers.IsOppositeColor(card, top) && RuleHelpers.IsOneRankLower(card, top);
    }

    private static bool IsPlayableSource(CardPile pile) =>
        !pile.IsEmpty
        && (pile.Name == Waste || pile.Name.StartsWith(TableauPrefix, StringComparison.OrdinalIgnoreCase))
        && pile.IsFaceUp(pile.Top);
}
