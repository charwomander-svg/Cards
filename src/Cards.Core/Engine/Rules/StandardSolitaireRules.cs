using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class StandardSolitaireRules : ICardGameRules
{
    private readonly int _tableauCount;
    private readonly int _foundationCount;
    private readonly int _drawCount;
    private readonly bool _alternatingTableau;

    public StandardSolitaireRules(
        string name,
        int tableauCount = 7,
        int foundationCount = 4,
        int drawCount = 1,
        bool alternatingTableau = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (tableauCount < 1)
            throw new ArgumentOutOfRangeException(nameof(tableauCount), "At least one tableau pile is required.");
        if (foundationCount < 1)
            throw new ArgumentOutOfRangeException(nameof(foundationCount), "At least one foundation pile is required.");
        if (drawCount < 1)
            throw new ArgumentOutOfRangeException(nameof(drawCount), "Draw count must be positive.");

        Name = name;
        _tableauCount = tableauCount;
        _foundationCount = foundationCount;
        _drawCount = drawCount;
        _alternatingTableau = alternatingTableau;
    }

    public string Name { get; }
    public int MinPlayers => 1;
    public int MaxPlayers => 1;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, false, true, true, false, false, false, false, true, false, new[] { "draw", "move" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} is a one-player game.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);

        session.AddPile("stock");
        session.AddPile("waste");
        for (int i = 1; i <= _foundationCount; i++)
            session.AddPile($"foundation{i}");
        for (int i = 1; i <= _tableauCount; i++)
            session.AddPile($"tableau{i}");

        for (int column = 1; column <= _tableauCount; column++)
        {
            CardPile tableau = session.GetPile($"tableau{column}");
            int cardsToDeal = Math.Min(column, deck.Count);
            for (int card = 0; card < cardsToDeal; card++)
                tableau.Add(deck.Draw());
        }

        session.GetPile("stock").AddRange(deck.Cards);
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var moves = new List<MoveDescriptor>();

        CardPile stock = session.GetPile("stock");
        if (!stock.IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", "waste", Math.Min(_drawCount, stock.Count)));

        foreach (CardPile source in session.Piles.Values.Where(IsPlayableSource))
        {
            foreach (CardPile foundation in session.Piles.Values.Where(IsFoundation))
                if (CanMoveToFoundation(source.Top, foundation))
                    moves.Add(new MoveDescriptor("move", source.Name, foundation.Name, Cards: new[] { source.Top.ToString() }));

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

        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
            return DrawFromStock(session);

        CardPile source = session.GetPile(move.Source);
        CardPile destination = session.GetPile(move.Destination ?? string.Empty);
        if (source.IsEmpty)
            return MoveResult.Failure("Source pile is empty.");

        Card card = source.Top;
        bool legal = IsFoundation(destination)
            ? CanMoveToFoundation(card, destination)
            : CanMoveToTableau(card, destination);

        if (!legal)
            return MoveResult.Failure("That move is not legal for this solitaire layout.");

        destination.Add(source.Draw());
        session.IsComplete = session.Piles.Values.Where(IsFoundation).Sum(p => p.Count) == 52;
        return MoveResult.Success();
    }

    private MoveResult DrawFromStock(CardGameSession session)
    {
        CardPile stock = session.GetPile("stock");
        CardPile waste = session.GetPile("waste");
        if (stock.IsEmpty)
            return MoveResult.Failure("The stock is empty.");

        int count = Math.Min(_drawCount, stock.Count);
        for (int i = 0; i < count; i++)
            waste.Add(stock.Draw());

        session.RecordDraw(0, "stock", count);
        session.Statistics.RecordPileAdded("waste", count, waste.Count);
        return MoveResult.Success($"Drew {count} card(s).");
    }

    private bool CanMoveToTableau(Card card, CardPile tableau)
    {
        if (tableau.IsEmpty)
            return card.Rank == Rank.King;

        Card top = tableau.Top;
        bool rankFits = RuleHelpers.IsOneRankLower(card, top);
        return _alternatingTableau
            ? rankFits && RuleHelpers.IsOppositeColor(card, top)
            : rankFits && card.Suit == top.Suit;
    }

    private static bool CanMoveToFoundation(Card card, CardPile foundation)
    {
        if (foundation.IsEmpty)
            return card.Rank == Rank.Ace;

        Card top = foundation.Top;
        return card.Suit == top.Suit && RuleHelpers.IsOneRankHigher(card, top);
    }

    private static bool IsPlayableSource(CardPile pile) => !pile.IsEmpty && (pile.Name == "waste" || IsTableau(pile));
    private static bool IsFoundation(CardPile pile) => pile.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase);
    private static bool IsTableau(CardPile pile) => pile.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase);
}
