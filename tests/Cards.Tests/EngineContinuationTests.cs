using Cards.Core.Engine;
using Cards.Core.Engine.Ai;
using Cards.Core.Engine.Rules;
using Cards.Core.Engine.Serialization;
using Xunit;

namespace Cards.Tests;

public class EngineContinuationTests
{
    [Fact]
    public void AiMoveSelector_PrefersHighValueMoveTypes()
    {
        var selector = new AiMoveSelector();
        var moves = new[]
        {
            new MoveDescriptor("draw", "stock"),
            new MoveDescriptor("capture", "Seven of Hearts", "Seven of Clubs"),
            new MoveDescriptor("play", "Ace of Spades", "trick"),
        };

        MoveDescriptor? selected = selector.ChooseMove(moves);

        Assert.NotNull(selected);
        Assert.Equal("capture", selected.Type);
    }

    [Fact]
    public void SessionTeams_RecordTeamScoresWhenPlayerScores()
    {
        var session = new CardGameSession("Teams", 4);
        session.AssignAlternatingTeams(2);

        session.RecordScore(2, 10);

        Assert.Equal(0, session.PlayerTeams[2]);
        Assert.Equal(10, session.TeamScores[0]);
    }

    [Fact]
    public void Snapshot_IncludesTeamState()
    {
        var session = new CardGameSession("Teams", 4);
        session.AssignAlternatingTeams(2);
        session.RecordScore(1, 5);

        GameSnapshot snapshot = session.CreateSnapshot();

        Assert.Equal(1, snapshot.PlayerTeams[1]);
        Assert.Equal(5, snapshot.TeamScores[1]);
    }

    [Fact]
    public void Capabilities_ExposeRuleMetadata()
    {
        var engine = new CardGameEngine();

        RuleCapabilities bridge = engine.GetCapabilities("Bridge");
        RuleCapabilities casino = engine.GetCapabilities("Casino");

        Assert.True(bridge.HasTricks);
        Assert.True(bridge.HasBidding);
        Assert.True(casino.HasCaptures);
        Assert.Contains("capture", casino.MoveTypes);
    }

    [Fact]
    public void SavedState_IncludesTeamState()
    {
        var session = new CardGameSession("Teams", 4);
        session.AssignAlternatingTeams(2);
        session.RecordScore(3, 12);

        SavedGameState saved = SessionSerializer.Save(session);

        Assert.Equal(1, saved.PlayerTeams[3]);
        Assert.Equal(12, saved.TeamScores[1]);
    }
}
