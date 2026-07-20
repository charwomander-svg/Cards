using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class FreeCellRules : ICardGameRules
{
    public FreeCellRules(string name = "FreeCell")
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
    }

    public string Name { get; }
    public int MinPlayers => 1;
    public int MaxPlayers => 1;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, false, false, true, false, false, false, false, false, false, new[] { "move" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} is a one-player game.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);
        for (int i = 1; i <= 4; i++)
        {
            session.AddPile($"cell{i}");
            session.AddPile($"foundation{i}");
        }
        for (int i = 1; i <= 8; i++)
            session.AddPile($"tableau{i}");

        int column = 1;
        while (!deck.IsEmpty)
        {
            session.GetPile($"tableau{column}").Add(deck.Draw());
            column = column == 8 ? 1 : column + 1;
        }

        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var moves = new List<MoveDescriptor>();
        CardPile[] sources = session.Piles.Values.Where(p => !p.IsEmpty && (IsTableau(p) || IsCell(p))).ToArray();

        foreach (CardPile source in sources)
        {
            foreach (CardPile foundation in session.Piles.Values.Where(IsFoundation))
                if (CanMoveToFoundation(source.Top, foundation))
                    moves.Add(new MoveDescriptor("move", source.Name, foundation.Name, Cards: new[] { source.Top.ToString() }));

            foreach (CardPile cell in session.Piles.Values.Where(p => IsCell(p) && p.IsEmpty))
                moves.Add(new MoveDescriptor("move", source.Name, cell.Name, Cards: new[] { source.Top.ToString() }));

            foreach (CardPile tableau in session.Piles.Values.Where(p => IsTableau(p) && p.Name != source.Name))
                if (CanMoveToTableau(source.Top, tableau))
                    moves.Add(new MoveDescriptor("move", source.Name, tableau.Name, Cards: new[] { source.Top.ToString() }));
        }

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        CardPile source = session.GetPile(move.Source);
        CardPile destination = session.GetPile(move.Destination ?? string.Empty);
        if (source.IsEmpty)
            return MoveResult.Failure("Source pile is empty.");

        Card card = source.Top;
        bool legal = IsFoundation(destination)
            ? CanMoveToFoundation(card, destination)
            : IsCell(destination)
                ? destination.IsEmpty
                : CanMoveToTableau(card, destination);

        if (!legal)
            return MoveResult.Failure("Illegal FreeCell move.");

        destination.Add(source.Draw());
        session.Statistics.RecordPileRemoved(source.Name, 1);
        if (IsFoundation(destination))
            session.RecordClear(0, destination.Name, 1);
        else
            session.RecordPlay(0, destination.Name, 1);
        session.IsComplete = session.Piles.Values.Where(IsFoundation).Sum(p => p.Count) == 52;
        return MoveResult.Success();
    }

    private static bool CanMoveToFoundation(Card card, CardPile foundation) =>
        foundation.IsEmpty ? card.Rank == Rank.Ace : card.Suit == foundation.Top.Suit && RuleHelpers.IsOneRankHigher(card, foundation.Top);

    private static bool CanMoveToTableau(Card card, CardPile tableau) =>
        tableau.IsEmpty || (RuleHelpers.IsOppositeColor(card, tableau.Top) && RuleHelpers.IsOneRankLower(card, tableau.Top));

    private static bool IsCell(CardPile pile) => pile.Name.StartsWith("cell", StringComparison.OrdinalIgnoreCase);
    private static bool IsFoundation(CardPile pile) => pile.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase);
    private static bool IsTableau(CardPile pile) => pile.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase);
}
