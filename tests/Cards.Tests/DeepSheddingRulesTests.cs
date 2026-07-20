using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Rules;
using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class DeepSheddingRulesTests
{
    [Fact]
    public void SheddingRulesV2_EightCanDeclareActiveSuit()
    {
        var rules = new SheddingRulesV2("Crazy Eights", cardsPerPlayer: 5);
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Eight));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("play", "Eight of Clubs", "discard", PlayerIndex: 0, DeclaredSuit: Suit.Spades));

        Assert.True(result.Succeeded);
        Assert.Equal(Suit.Spades, rules.GetState(session).ActiveSuit);
    }

    [Fact]
    public void SheddingRulesV2_TwoCreatesDrawPenalty()
    {
        var rules = new SheddingRulesV2("Crazy Eights", cardsPerPlayer: 5);
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.GetPile("discard").Clear();
        session.GetPile("discard").Add(new Card(Suit.Clubs, Rank.Seven));
        session.Hands[0].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Two));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("play", "Two of Clubs", "discard", PlayerIndex: 0));

        Assert.True(result.Succeeded);
        Assert.Equal(2, rules.GetState(session).PendingDrawCount);
    }

    [Fact]
    public void SheddingRulesV2_DrawConsumesPenalty()
    {
        var rules = new SheddingRulesV2("Crazy Eights", cardsPerPlayer: 5);
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.GetPile("discard").Clear();
        session.GetPile("discard").Add(new Card(Suit.Clubs, Rank.Seven));
        session.Hands[0].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Two));
        rules.ApplyMove(session, new MoveDescriptor("play", "Two of Clubs", "discard", PlayerIndex: 0));
        int startingCount = session.Hands[1].Count;

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("draw", "stock", PlayerIndex: 1));

        Assert.True(result.Succeeded);
        Assert.Equal(startingCount + 2, session.Hands[1].Count);
        Assert.Equal(0, rules.GetState(session).PendingDrawCount);
    }

    [Fact]
    public void SheddingRulesV2_QueenSkipsNextPlayer()
    {
        var rules = new SheddingRulesV2("Crazy Eights", minPlayers: 3, maxPlayers: 3, cardsPerPlayer: 5);
        CardGameSession session = rules.CreateSession(3, new Random(42));
        session.GetPile("discard").Clear();
        session.GetPile("discard").Add(new Card(Suit.Hearts, Rank.Five));
        session.Hands[0].Clear();
        session.Hands[0].AddCard(new Card(Suit.Hearts, Rank.Queen));

        rules.ApplyMove(session, new MoveDescriptor("play", "Queen of Hearts", "discard", PlayerIndex: 0));

        Assert.Equal(2, session.CurrentPlayerIndex);
    }

    [Fact]
    public void GoFish_RequestTransfersMatchingCards()
    {
        var rules = new GoFishRules();
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        session.Hands[1].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Seven));
        session.Hands[1].AddCard(new Card(Suit.Hearts, Rank.Seven));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("request", "hand", PlayerIndex: 0, TargetPlayerIndex: 1, RequestedRank: Rank.Seven));

        Assert.True(result.Succeeded);
        Assert.Equal(2, session.Hands[0].Count);
        Assert.True(session.Hands[1].IsEmpty);
    }

    [Fact]
    public void GoFish_BookScoresAndRemovesFourOfKind()
    {
        var rules = new GoFishRules();
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        session.Hands[1].Clear();
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.Seven));
        session.Hands[0].AddCard(new Card(Suit.Diamonds, Rank.Seven));
        session.Hands[1].AddCard(new Card(Suit.Hearts, Rank.Seven));
        session.Hands[1].AddCard(new Card(Suit.Spades, Rank.Seven));

        rules.ApplyMove(session, new MoveDescriptor("request", "hand", PlayerIndex: 0, TargetPlayerIndex: 1, RequestedRank: Rank.Seven));

        Assert.True(session.Hands[0].IsEmpty);
        Assert.Equal(1, session.Scores[0]);
    }

    [Fact]
    public void ProfileCatalog_UsesDeepSheddingRules()
    {
        Assert.IsType<SheddingRulesV2>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Crazy Eights"]));
        Assert.IsType<GoFishRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Go Fish"]));
    }
}
