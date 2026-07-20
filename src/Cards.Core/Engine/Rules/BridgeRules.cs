using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class BridgeRules : TrickTakingRulesBase
{
    public BridgeRules(string name = "Bridge") : base(name, 2, 4, 13)
    {
    }

    public override RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, true, true, false, false, true, false, false, true, false, false, new[] { "bid", "play" });

    protected override void Deal(CardGameSession session, Random? random)
    {
        var deck = new Deck();
        deck.Shuffle(random);
        session.DealToEach(deck, CardsPerPlayer);
    }

    protected override int TrickScore(IReadOnlyList<TrickPlay> trick) => 1;
}
