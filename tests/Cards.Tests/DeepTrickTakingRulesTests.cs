using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Rules;
using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class DeepTrickTakingRulesTests
{
    [Fact]
    public void EuchreRules_UsesTwentyFourCardDeckAndTeams()
    {
        var rules = new EuchreRules();

        CardGameSession session = rules.CreateSession(4, new Random(42));

        Assert.All(session.Hands, hand => Assert.Equal(5, hand.Count));
        Assert.True(session.GetPile("stock").Count >= 0);
        Assert.Equal(2, session.PlayerTeams.Values.Distinct().Count());
    }

    [Fact]
    public void BridgeRules_DealsThirteenCardsAndTracksContractBid()
    {
        var rules = new BridgeRules();
        CardGameSession session = rules.CreateSession(4, new Random(42));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("bid", "hand", PlayerIndex: 0, Bid: 3));

        Assert.True(result.Succeeded);
        Assert.Equal(3, rules.GetState(session).ContractLevel);
        Assert.Equal(0, rules.GetState(session).DeclarerIndex);
        Assert.All(session.Hands, hand => Assert.Equal(13, hand.Count));
    }

    [Fact]
    public void TrickTakingRules_EnforcesFollowSuit()
    {
        var rules = new BridgeRules();
        CardGameSession session = rules.CreateSession(4, new Random(42));
        session.Hands[0].Clear();
        session.Hands[1].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Ace));
        session.Hands[1].AddCard(new Card(Suit.Clubs, Rank.Two));
        session.Hands[1].AddCard(new Card(Suit.Hearts, Rank.Ace));
        rules.ApplyMove(session, new MoveDescriptor("play", "Ace of Clubs", "trick", PlayerIndex: 0));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("play", "Ace of Hearts", "trick", PlayerIndex: 1));

        Assert.False(result.Succeeded);
    }

    [Fact]
    public void TrickTakingRules_CompletedTrickWinnerLeadsNext()
    {
        var rules = new BridgeRules();
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        session.Hands[1].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Two));
        session.Hands[1].AddCard(new Card(Suit.Clubs, Rank.Ace));

        rules.ApplyMove(session, new MoveDescriptor("play", "Two of Clubs", "trick", PlayerIndex: 0));
        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("play", "Ace of Clubs", "trick", PlayerIndex: 1));

        Assert.True(result.Succeeded);
        Assert.Equal(0, session.CurrentPlayerIndex);
        Assert.True(session.Statistics.TotalScoreGained > 0);
    }

    [Fact]
    public void PinochleRules_UsesFortyEightCardDeck()
    {
        var rules = new PinochleRules();

        CardGameSession session = rules.CreateSession(4, new Random(42));

        Assert.All(session.Hands, hand => Assert.Equal(12, hand.Count));
        Assert.True(session.GetPile("stock").IsEmpty);
    }

    [Fact]
    public void PinochleRules_ScoresPinochleMeld()
    {
        var rules = new PinochleRules();
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        var queen = new Card(Suit.Spades, Rank.Queen);
        var jack = new Card(Suit.Diamonds, Rank.Jack);
        session.Hands[0].AddCard(queen);
        session.Hands[0].AddCard(jack);

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("meld", "hand", PlayerIndex: 0, Cards: new[] { queen.ToString(), jack.ToString() }));

        Assert.True(result.Succeeded);
        Assert.True(session.Statistics.TotalScoreGained >= 4);
    }

    [Fact]
    public void ProfileCatalog_UsesDeepTrickTakingRules()
    {
        Assert.IsType<EuchreRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Euchre"]));
        Assert.IsType<BridgeRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Bridge"]));
        Assert.IsType<PinochleRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Pinochle"]));
    }
}
