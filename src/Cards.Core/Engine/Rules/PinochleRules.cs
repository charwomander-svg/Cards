using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class PinochleRules : TrickTakingRulesBase
{
    public PinochleRules(string name = "Pinochle") : base(name, 2, 4, 12)
    {
    }

    public override RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, true, true, true, false, true, true, false, true, false, false, new[] { "bid", "meld", "play" });

    protected override void Deal(CardGameSession session, Random? random)
    {
        List<Card> cards = new();
        for (int copy = 0; copy < 2; copy++)
            cards.AddRange(Enum.GetValues<Suit>()
                .SelectMany(suit => new[] { Rank.Nine, Rank.Jack, Rank.Queen, Rank.King, Rank.Ten, Rank.Ace }.Select(rank => new Card(suit, rank))));
        Shuffle(cards, random);
        int index = 0;
        for (int i = 0; i < CardsPerPlayer; i++)
            foreach (Hand hand in session.Hands)
                hand.AddCard(cards[index++]);
        session.AddPile("stock").AddRange(cards.Skip(index));
    }

    public override MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        if (move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase))
        {
            int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
            Card[] cards = RummyRules.ResolveCards(session.Hands[player], move.Cards);
            int score = ScoreMeld(cards);
            if (score == 0)
                return MoveResult.Failure("Unsupported Pinochle meld.");
            session.RecordScore(player, score);
            return session.ApplyGenericAction(move);
        }

        return base.ApplyMove(session, move);
    }

    protected override int TrickScore(IReadOnlyList<TrickPlay> trick) =>
        trick.Sum(play => play.Card.Rank is Rank.Ace or Rank.Ten or Rank.King ? 1 : 0);

    public static int ScoreMeld(IReadOnlyList<Card> cards)
    {
        if (cards.Any(card => card.Rank == Rank.Queen && card.Suit == Suit.Spades)
            && cards.Any(card => card.Rank == Rank.Jack && card.Suit == Suit.Diamonds))
            return 4;
        if (cards.Count >= 4 && cards.Select(card => card.Rank).Distinct().Count() == 1)
            return 8;
        if (cards.Count >= 4 && cards.Select(card => card.Suit).Distinct().Count() == 1)
            return 4;
        return 0;
    }

    private static void Shuffle(List<Card> cards, Random? random)
    {
        var rng = random ?? Random.Shared;
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
}
