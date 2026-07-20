using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Rules;
using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class RummyCanastaRulesTests
{
    [Fact]
    public void RummyRules_ValidatesSetsAndRuns()
    {
        Card[] set =
        {
            new(Suit.Clubs, Rank.Seven),
            new(Suit.Diamonds, Rank.Seven),
            new(Suit.Spades, Rank.Seven),
        };
        Card[] run =
        {
            new(Suit.Hearts, Rank.Four),
            new(Suit.Hearts, Rank.Five),
            new(Suit.Hearts, Rank.Six),
        };

        Assert.True(RummyRules.IsValidMeld(set));
        Assert.True(RummyRules.IsValidMeld(run));
    }

    [Fact]
    public void RummyRules_RejectsInvalidMeld()
    {
        Card[] cards =
        {
            new(Suit.Clubs, Rank.Seven),
            new(Suit.Diamonds, Rank.Eight),
            new(Suit.Spades, Rank.Nine),
        };

        Assert.False(RummyRules.IsValidMeld(cards));
    }

    [Fact]
    public void RummyRules_CalculatesDeadwood()
    {
        Card[] hand =
        {
            new(Suit.Clubs, Rank.Seven),
            new(Suit.Diamonds, Rank.Seven),
            new(Suit.Spades, Rank.Seven),
            new(Suit.Hearts, Rank.King),
        };

        Assert.Equal(10, RummyRules.CalculateDeadwood(hand));
    }

    [Fact]
    public void RummyRules_MeldMoveScoresValidMeld()
    {
        var rules = new RummyRules("Rummy", cardsPerPlayer: 7);
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        var cards = new[]
        {
            new Card(Suit.Clubs, Rank.Seven),
            new Card(Suit.Diamonds, Rank.Seven),
            new Card(Suit.Spades, Rank.Seven),
        };
        foreach (Card card in cards)
            session.Hands[0].AddCard(card);

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("meld", "hand", PlayerIndex: 0, Cards: cards.Select(card => card.ToString()).ToArray()));

        Assert.True(result.Succeeded);
        Assert.True(session.Statistics.TotalScoreGained > 0);
    }

    [Fact]
    public void CanastaRules_UsesTwoDecksAndDealsEleven()
    {
        var rules = new CanastaRules();

        CardGameSession session = rules.CreateSession(4, new Random(42));

        Assert.All(session.Hands, hand => Assert.Equal(11, hand.Count));
        Assert.Equal(59, session.GetPile("stock").Count);
        Assert.Equal(1, session.GetPile("discard").Count);
        Assert.Equal(2, session.TeamScores.Count);
    }

    [Fact]
    public void CanastaRules_ValidatesCanastaMeld()
    {
        Card[] cards = Enumerable.Range(0, 7).Select(_ => new Card(Suit.Hearts, Rank.Queen)).ToArray();

        Assert.True(CanastaRules.IsValidCanastaMeld(cards, 7));
    }

    [Fact]
    public void CanastaRules_CanastaMoveAddsBonusScore()
    {
        var rules = new CanastaRules();
        CardGameSession session = rules.CreateSession(2, new Random(42));
        session.Hands[0].Clear();
        Card[] cards = Enumerable.Range(0, 7).Select(_ => new Card(Suit.Hearts, Rank.Queen)).ToArray();
        foreach (Card card in cards)
            session.Hands[0].AddCard(card);

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("canasta", "hand", PlayerIndex: 0, Cards: cards.Select(card => card.ToString()).ToArray()));

        Assert.True(result.Succeeded);
        Assert.True(session.Statistics.TotalScoreGained >= 500);
    }

    [Fact]
    public void ProfileCatalog_UsesDeepRummyAndCanastaRules()
    {
        Assert.IsType<RummyRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Gin Rummy"]));
        Assert.IsType<CanastaRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Canasta"]));
        Assert.IsType<CanastaRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Samba Canasta"]));
    }
}
