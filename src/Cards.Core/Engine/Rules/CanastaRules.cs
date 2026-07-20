using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class CanastaRules : ICardGameRules
{
    public string Name { get; }
    public int MinPlayers => 2;
    public int MaxPlayers => 6;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, true, true, true, false, false, true, false, false, true, true, new[] { "draw", "discard", "meld", "canasta" });

    public CanastaRules(string name = "Canasta")
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
    }

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} supports {MinPlayers}-{MaxPlayers} players.");

        var session = new CardGameSession(Name, playerCount);
        List<Card> cards = new();
        for (int i = 0; i < 2; i++)
            cards.AddRange(new Deck().Cards);
        var rng = random ?? Random.Shared;
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        int index = 0;
        for (int round = 0; round < 11; round++)
            foreach (Hand hand in session.Hands)
                hand.AddCard(cards[index++]);
        session.AddPile("stock").AddRange(cards.Skip(index));
        session.AddPile("discard").Add(session.GetPile("stock").Draw());
        session.AssignAlternatingTeams(playerCount >= 4 ? 2 : playerCount);
        foreach (int teamIndex in session.PlayerTeams.Values.Distinct())
            session.TeamScores[teamIndex] = 0;
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        int player = session.CurrentPlayerIndex;
        var moves = new List<MoveDescriptor>();
        if (!session.GetPile("stock").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", PlayerIndex: player, Count: 2));
        if (!session.GetPile("discard").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "discard", PlayerIndex: player));
        moves.AddRange(session.Hands[player].Cards.Select(card => new MoveDescriptor("discard", card.ToString(), "discard", PlayerIndex: player)));
        moves.AddRange(FindRankMelds(session.Hands[player].Cards, 3).Select(meld => new MoveDescriptor("meld", "hand", PlayerIndex: player, Cards: meld.Select(card => card.ToString()).ToArray())));
        moves.AddRange(FindRankMelds(session.Hands[player].Cards, 7).Select(meld => new MoveDescriptor("canasta", "hand", PlayerIndex: player, Cards: meld.Select(card => card.ToString()).ToArray())));
        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);
        int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (player != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        Hand hand = session.Hands[player];
        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
        {
            CardPile source = session.GetPile(move.Source);
            int count = source.Name == "stock" ? Math.Min(2, source.Count) : 1;
            for (int i = 0; i < count; i++)
                hand.AddCard(source.Draw());
            session.RecordDraw(player, source.Name, count);
            return MoveResult.Success($"Drew {count} card(s).");
        }

        if (move.Type.Equals("discard", StringComparison.OrdinalIgnoreCase))
        {
            Card? card = RummyRules.FindCard(hand, move.Source);
            if (card is null)
                return MoveResult.Failure("The selected card is not in the player's hand.");
            hand.PlayCard(card);
            session.GetPile("discard").Add(card);
            session.RecordDiscard(player, "discard", 1);
            session.AdvanceTurn();
            return MoveResult.Success($"{card} discarded.");
        }

        if (move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase) || move.Type.Equals("canasta", StringComparison.OrdinalIgnoreCase))
        {
            Card[] cards = RummyRules.ResolveCards(hand, move.Cards);
            int required = move.Type.Equals("canasta", StringComparison.OrdinalIgnoreCase) ? 7 : 3;
            if (!IsValidCanastaMeld(cards, required))
                return MoveResult.Failure($"Canasta meld requires at least {required} cards of the same rank.");

            int score = cards.Sum(CardValue) + (required == 7 ? 500 : 0);
            session.RecordScore(player, score);
            return session.ApplyGenericAction(new MoveDescriptor("meld", "hand", PlayerIndex: player, Cards: move.Cards));
        }

        return MoveResult.Failure($"Unsupported Canasta move '{move.Type}'.");
    }

    public static bool IsValidCanastaMeld(IReadOnlyList<Card> cards, int requiredCount = 3) =>
        cards.Count >= requiredCount && cards.All(card => card.Rank == cards[0].Rank);

    private static IReadOnlyList<IReadOnlyList<Card>> FindRankMelds(IReadOnlyList<Card> cards, int minimum) =>
        cards.GroupBy(card => card.Rank).Where(group => group.Count() >= minimum).Select(group => (IReadOnlyList<Card>)group.ToArray()).ToArray();

    private static int CardValue(Card card) => card.Rank switch
    {
        Rank.Ace => 20,
        Rank.Eight or Rank.Nine or Rank.Ten or Rank.Jack or Rank.Queen or Rank.King => 10,
        _ => 5
    };
}
