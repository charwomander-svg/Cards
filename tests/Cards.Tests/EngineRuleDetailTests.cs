using Cards.Core.Engine;
using Cards.Core.Engine.Actions;
using Cards.Core.Engine.Rules;
using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class EngineRuleDetailTests
{
    [Fact]
    public void CardPile_TracksFaceUpState()
    {
        var pile = new CardPile("tableau");
        var card = new Card(Suit.Spades, Rank.Ace);
        pile.Add(card);

        pile.SetFaceUp(card, false);

        Assert.False(pile.IsFaceUp(card));
        Assert.False(pile.VisibleCards[0].IsFaceUp);
        pile.FlipTop();
        Assert.True(pile.IsFaceUp(card));
    }

    [Fact]
    public void GenericBid_RecordsBidAndAdvancesTurn()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));

        MoveResult result = engine.ApplyMove(session, new MoveDescriptor("bid", "hand", PlayerIndex: 0, Bid: 3));

        Assert.True(result.Succeeded);
        Assert.Equal(3, session.Bids[0]);
        Assert.Equal(1, session.CurrentPlayerIndex);
    }

    [Fact]
    public void GenericPass_AdvancesTurn()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));

        MoveResult result = engine.ApplyMove(session, new MoveDescriptor("pass", "hand", PlayerIndex: 0));

        Assert.True(result.Succeeded);
        Assert.Equal(1, session.CurrentPlayerIndex);
    }

    [Fact]
    public void GenericMeld_RecordsCardsAndScore()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Pinochle", 2, new Random(42));
        string[] cards = session.Hands[0].Cards.Take(2).Select(card => card.ToString()).ToArray();

        MoveResult result = engine.ApplyMove(session, new MoveDescriptor("meld", "hand", PlayerIndex: 0, Cards: cards));

        Assert.True(result.Succeeded);
        Assert.Single(session.Melds[0]);
        Assert.True(session.Statistics.TotalScoreGained > 0);
    }

    [Fact]
    public void Knock_CompletesHand()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Gin Rummy", 2, new Random(42));

        MoveResult result = engine.ApplyMove(session, new MoveDescriptor("knock", "hand", PlayerIndex: 0));

        Assert.True(result.Succeeded);
        Assert.True(session.IsComplete);
    }

    [Fact]
    public void StartNextRound_ClearsTransientStateAndPreservesScores()
    {
        var session = new CardGameSession("Round Game", 2);
        session.Scores[0] = 10;
        session.AddPile("stock").Add(new Card(Suit.Hearts, Rank.Ace));
        session.Hands[0].AddCard(new Card(Suit.Clubs, Rank.King));

        session.StartNextRound();

        Assert.Equal(10, session.Scores[0]);
        Assert.True(session.Hands[0].IsEmpty);
        Assert.True(session.GetPile("stock").IsEmpty);
        Assert.Equal(2, session.Round);
    }

    [Fact]
    public void ActionDescriptionProvider_DescribesGenericActions()
    {
        ActionDescription description = ActionDescriptionProvider.Describe(new MoveDescriptor("bid", "hand", Bid: 4));

        Assert.Equal("Bid 4", description.Label);
    }
}
