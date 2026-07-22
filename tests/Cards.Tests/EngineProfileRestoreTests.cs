using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Serialization;
using Xunit;

namespace Cards.Tests;

public class EngineProfileRestoreTests
{
    [Fact]
    public void RuleProfileCatalog_HasBroadPlayableCoverage()
    {
        Assert.True(RuleProfileCatalog.Profiles.Count >= 100);
        Assert.Contains(RuleProfileCatalog.Profiles, profile => profile.GameName == "Contract Bridge");
        Assert.Contains(RuleProfileCatalog.Profiles, profile => profile.GameName == "Uno");
        Assert.Contains(RuleProfileCatalog.Profiles, profile => profile.GameName == "Double FreeCell");
        Assert.Contains(RuleProfileCatalog.Profiles, profile => profile.GameName == "Cassino");
    }

    [Theory]
    [InlineData("Contract Bridge", 4)]
    [InlineData("Uno", 4)]
    [InlineData("Double FreeCell", 1)]
    [InlineData("Cassino", 2)]
    public void ExpandedProfiles_StartGames(string gameName, int players)
    {
        var engine = new CardGameEngine();

        CardGameSession session = engine.StartGame(gameName, players, new Random(42));

        Assert.Equal(gameName, session.GameName);
        Assert.True(engine.GetLegalMoves(session).Count > 0);
    }

    [Fact]
    public void SessionSerializer_RestoresMutableSessionState()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Gin Rummy", 2, new Random(42));
        session.AssignAlternatingTeams(2);
        engine.ApplyMove(session, new("draw", "stock", PlayerIndex: 0));
        string json = SessionSerializer.ToJson(session);

        CardGameSession restored = SessionSerializer.RestoreJson(json);

        Assert.Equal(session.GameName, restored.GameName);
        Assert.Equal(session.CurrentPlayerIndex, restored.CurrentPlayerIndex);
        Assert.Equal(session.GetPile("stock").Count, restored.GetPile("stock").Count);
        Assert.Equal(session.Hands[0].Count, restored.Hands[0].Count);
        Assert.Equal(session.PlayerTeams[0], restored.PlayerTeams[0]);
    }
}
