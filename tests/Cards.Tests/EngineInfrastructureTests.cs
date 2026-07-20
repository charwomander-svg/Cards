using Cards.Core.Engine;
using Cards.Core.Engine.Actions;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Replay;
using Cards.Core.Engine.Rules;
using Cards.Core.Engine.Serialization;
using Cards.Core.Engine.Simulation;
using Xunit;

namespace Cards.Tests;

public class EngineInfrastructureTests
{
    [Fact]
    public void RuleProfiles_CreateRulesForEveryProfile()
    {
        foreach (RuleProfile profile in RuleProfileCatalog.Profiles)
        {
            Assert.True(RuleProfileCatalog.TryCreateRules(profile.GameName, out ICardGameRules rules));
            Assert.Equal(profile.GameName, rules.Name);
        }
    }

    [Fact]
    public void SessionSerializer_RoundTripsSnapshotData()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));
        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        string json = SessionSerializer.ToJson(session);
        SavedGameState saved = SessionSerializer.FromJson(json);

        Assert.Equal("Klondike", saved.GameName);
        Assert.Single(saved.History);
        Assert.Equal(23, saved.Piles["stock"].Count);
        Assert.Single(saved.Piles["waste"]);
    }

    [Fact]
    public void SessionReplay_ReplaysDeterministicMoves()
    {
        var engine = new CardGameEngine();
        var replay = new SessionReplay(engine);
        var moves = new[]
        {
            new MoveDescriptor("draw", "stock", "waste"),
            new MoveDescriptor("draw", "stock", "waste"),
        };

        ReplayResult result = replay.Replay("Klondike", 1, 42, moves);

        Assert.Equal(2, result.Results.Count);
        Assert.All(result.Results, moveResult => Assert.True(moveResult.Succeeded));
        Assert.Equal(22, result.Session.GetPile("stock").Count);
        Assert.Equal(2, result.Session.History.Count);
    }

    [Fact]
    public void SessionReplay_UndoLast_RebuildsWithoutLastMove()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));
        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));
        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        ReplayResult undone = new SessionReplay(engine).UndoLast(session, 42);

        Assert.Single(undone.Session.History);
        Assert.Equal(23, undone.Session.GetPile("stock").Count);
        Assert.Equal(1, undone.Session.GetPile("waste").Count);
    }

    [Fact]
    public void ActionDescriptionProvider_DescribesLegalMovesForUi()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        ActionDescription description = ActionDescriptionProvider.Describe(engine.GetLegalMoves(session))[0];

        Assert.Equal("Draw from stock", description.Label);
        Assert.NotEmpty(description.HelpText);
    }

    [Fact]
    public void GameSimulator_RunsDeterministicSmokeSimulation()
    {
        var simulator = new GameSimulator(new CardGameEngine());

        SimulationResult result = simulator.Run("Klondike", 1, 42, 5);

        Assert.Equal("Klondike", result.GameName);
        Assert.True(result.MovesAttempted > 0);
        Assert.Equal(result.MovesAttempted, result.Statistics.MovesAttempted);
    }
}
