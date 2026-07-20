using Cards.Core.Engine;
using Cards.Core.Engine.Endings;
using Cards.Core.Engine.Rounds;
using Cards.Core.Engine.Scoring;
using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class EngineScoringEndingTests
{
    [Fact]
    public void ScoreCalculator_CalculatesHandValueWithFaceCards()
    {
        var cards = new[]
        {
            new Card(Suit.Hearts, Rank.Ace),
            new Card(Suit.Spades, Rank.King),
            new Card(Suit.Clubs, Rank.Seven),
        };

        Assert.Equal(18, ScoreCalculator.CalculateHandValue(cards));
    }

    [Fact]
    public void ScoreCalculator_CalculatesCaptureCount()
    {
        var session = new CardGameSession("Capture", 2);
        session.AddPile("captured").Add(new Card(Suit.Hearts, Rank.Ace));
        session.GetPile("captured").Add(new Card(Suit.Clubs, Rank.Ace));

        int score = ScoreCalculator.CalculateScore(session, 0, ScorePolicy.CaptureCount);

        Assert.Equal(2, score);
    }

    [Fact]
    public void EndConditionEvaluator_DetectsTargetScore()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));
        session.Scores[0] = 100;

        EndConditionResult result = engine.EvaluateEndConditions(session, targetScore: 100);

        Assert.True(result.IsMet);
        Assert.Equal(EndCondition.TargetScore, result.Condition);
    }

    [Fact]
    public void EndConditionEvaluator_DetectsFoundationCompletion()
    {
        var engine = new CardGameEngine();
        var session = new CardGameSession("Solitaire", 1);
        CardPile foundation = session.AddPile("foundation1");
        foreach (Suit suit in Enum.GetValues<Suit>())
            foreach (Rank rank in Enum.GetValues<Rank>())
                foundation.Add(new Card(suit, rank));

        EndConditionResult result = engine.EvaluateEndConditions(session);

        Assert.True(result.IsMet);
        Assert.Equal(EndCondition.FoundationComplete, result.Condition);
    }

    [Fact]
    public void RoundSummaryBuilder_ReturnsScoresStatsAndEnding()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));
        session.Scores[0] = 50;

        RoundSummary summary = engine.BuildRoundSummary(session, targetScore: 50);

        Assert.True(summary.IsComplete);
        Assert.Equal(50, summary.Scores[0]);
        Assert.Equal(EndCondition.TargetScore, summary.EndCondition.Condition);
        Assert.Equal("Bridge", summary.GameName);
    }
}
