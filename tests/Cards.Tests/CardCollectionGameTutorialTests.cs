using Cards.Engine;
using Cards.Xbox;
using Xunit;

namespace Cards.Tests;

public class CardCollectionGameTutorialTests
{
    [Fact]
    public void Update_PlayerTurn_DrawsTutorialPrompt()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(playerCount: 2, input, renderer);
        game.Initialize();
        game.Deal();

        game.Update();

        Assert.Contains(renderer.GetFrameDrawLog(), entry => entry.Contains("Press Y for card game tutorials."));
    }

    [Fact]
    public void Update_YButtonHeld_DrawsTutorialRules()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(playerCount: 2, input, renderer);
        game.Initialize();
        game.Deal();
        input.SetButtonState(XboxButton.Y, ButtonState.Pressed);

        game.Update();

        var log = renderer.GetFrameDrawLog();
        Assert.Contains(log, entry => entry.Contains("Tutorials: Left/Right choose game, B returns"));
        Assert.Contains(log, entry => entry.Contains("War"));
        Assert.Contains(log, entry => entry.Contains("Goal:"));
    }
}
