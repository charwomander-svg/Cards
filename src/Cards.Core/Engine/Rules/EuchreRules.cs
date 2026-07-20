using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class EuchreRules : TrickTakingRulesBase
{
    public EuchreRules() : base("Euchre", 2, 4, 5)
    {
    }

    public override RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, true, true, true, false, true, false, false, true, false, false, new[] { "bid", "play" });

    protected override void Deal(CardGameSession session, Random? random)
    {
        List<Card> cards = Enum.GetValues<Suit>()
            .SelectMany(suit => Enum.GetValues<Rank>().Where(rank => rank >= Rank.Nine).Select(rank => new Card(suit, rank)))
            .ToList();
        Shuffle(cards, random);
        int index = 0;
        for (int i = 0; i < CardsPerPlayer; i++)
            foreach (Hand hand in session.Hands)
                hand.AddCard(cards[index++]);
        session.AddPile("stock").AddRange(cards.Skip(index));
    }

    protected override int RankStrength(Card card)
    {
        TrickTakingState? state = null;
        int value = card.Rank switch
        {
            Rank.Jack => 20,
            Rank.Ace => 19,
            Rank.King => 18,
            Rank.Queen => 17,
            Rank.Ten => 16,
            Rank.Nine => 15,
            _ => (int)card.Rank
        };
        _ = state;
        return value;
    }

    protected override int TrickScore(IReadOnlyList<TrickPlay> trick) => 1;

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
