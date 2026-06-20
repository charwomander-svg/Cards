using Cards.Core;
using Xunit;

namespace Cards.Tests;

public class GameStateTests
{
    [Fact]
    public void NewGameState_DefaultsToMainMenuPhase()
    {
        var state = new GameState();
        Assert.Equal(GamePhase.MainMenu, state.Phase);
    }

    [Fact]
    public void Reset_ClearsScoresAndRound()
    {
        var state = new GameState();
        state.Scores[0] = 100;
        state.Round = 5;
        state.Phase = GamePhase.GameOver;

        state.Reset();

        Assert.Equal(GamePhase.MainMenu, state.Phase);
        Assert.Equal(1, state.Round);
        Assert.Empty(state.Scores);
    }
}
