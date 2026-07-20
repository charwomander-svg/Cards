using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class MatchingSolitaireRules : ICardGameRules
{
    private readonly int _targetTotal;
    private readonly int _tableauCount;

    public MatchingSolitaireRules(string name, int targetTotal = 13, int tableauCount = 12)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (targetTotal < 2)
            throw new ArgumentOutOfRangeException(nameof(targetTotal), "Target total must be at least two.");
        if (tableauCount < 2)
            throw new ArgumentOutOfRangeException(nameof(tableauCount), "At least two tableau piles are required.");

        Name = name;
        _targetTotal = targetTotal;
        _tableauCount = tableauCount;
    }

    public string Name { get; }
    public int MinPlayers => 1;
    public int MaxPlayers => 1;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, false, true, true, false, false, false, false, true, false, new[] { "clear-single", "clear-pair", "deal" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} is a one-player game.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);

        session.AddPile("stock");
        session.AddPile("cleared");
        for (int i = 1; i <= _tableauCount; i++)
            session.AddPile($"tableau{i}").Add(deck.Draw());

        session.GetPile("stock").AddRange(deck.Cards);
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var moves = new List<MoveDescriptor>();
        CardPile[] tableaus = session.Piles.Values.Where(p => p.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase) && !p.IsEmpty).ToArray();

        foreach (CardPile pile in tableaus)
            if (RankValue(pile.Top) == _targetTotal)
                moves.Add(new MoveDescriptor("clear-single", pile.Name, "cleared", Cards: new[] { pile.Top.ToString() }));

        for (int i = 0; i < tableaus.Length; i++)
            for (int j = i + 1; j < tableaus.Length; j++)
                if (RankValue(tableaus[i].Top) + RankValue(tableaus[j].Top) == _targetTotal)
                    moves.Add(new MoveDescriptor("clear-pair", tableaus[i].Name, tableaus[j].Name, 2, Cards: new[] { tableaus[i].Top.ToString(), tableaus[j].Top.ToString() }));

        if (!session.GetPile("stock").IsEmpty && session.Piles.Values.Any(p => p.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase) && p.IsEmpty))
            moves.Add(new MoveDescriptor("deal", "stock", "tableau"));

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        if (move.Type.Equals("deal", StringComparison.OrdinalIgnoreCase))
            return RefillEmptyTableaus(session);

        CardPile cleared = session.GetPile("cleared");
        if (move.Type.Equals("clear-single", StringComparison.OrdinalIgnoreCase))
        {
            CardPile source = session.GetPile(move.Source);
            if (source.IsEmpty || RankValue(source.Top) != _targetTotal)
                return MoveResult.Failure("Selected card does not match the clear target.");

            cleared.Add(source.Draw());
            session.Statistics.RecordPileRemoved(source.Name, 1);
            session.RecordClear(0, "cleared", 1);
            UpdateCompletion(session);
            return MoveResult.Success("Cleared one card.");
        }

        if (move.Type.Equals("clear-pair", StringComparison.OrdinalIgnoreCase))
        {
            CardPile first = session.GetPile(move.Source);
            CardPile second = session.GetPile(move.Destination ?? string.Empty);
            if (first.IsEmpty || second.IsEmpty || RankValue(first.Top) + RankValue(second.Top) != _targetTotal)
                return MoveResult.Failure("Selected cards do not add to the target.");

            cleared.Add(first.Draw());
            cleared.Add(second.Draw());
            session.Statistics.RecordPileRemoved(first.Name, 1);
            session.Statistics.RecordPileRemoved(second.Name, 1);
            session.RecordClear(0, "cleared", 2);
            UpdateCompletion(session);
            return MoveResult.Success("Cleared a pair.");
        }

        return MoveResult.Failure($"Unsupported move '{move.Type}'.");
    }

    private MoveResult RefillEmptyTableaus(CardGameSession session)
    {
        CardPile stock = session.GetPile("stock");
        if (stock.IsEmpty)
            return MoveResult.Failure("The stock is empty.");

        int dealt = 0;
        foreach (CardPile tableau in session.Piles.Values.Where(p => p.Name.StartsWith("tableau", StringComparison.OrdinalIgnoreCase) && p.IsEmpty))
        {
            if (stock.IsEmpty)
                break;

            tableau.Add(stock.Draw());
            session.RecordDraw(0, "stock", 1);
            session.Statistics.RecordPileAdded(tableau.Name, 1, tableau.Count);
            dealt++;
        }

        return dealt == 0 ? MoveResult.Failure("No empty tableau piles are available.") : MoveResult.Success($"Dealt {dealt} card(s).");
    }

    private static int RankValue(Card card) => card.Rank switch
    {
        Rank.Jack => 11,
        Rank.Queen => 12,
        Rank.King => 13,
        _ => (int)card.Rank
    };

    private static void UpdateCompletion(CardGameSession session)
    {
        session.IsComplete = session.GetPile("cleared").Count == 52;
    }
}
