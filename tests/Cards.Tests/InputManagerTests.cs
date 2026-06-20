using Cards.Engine;
using Xunit;

namespace Cards.Tests;

public class InputManagerTests
{
    [Fact]
    public void IsDown_DefaultState_ReturnsFalse()
    {
        var input = new InputManager();
        Assert.False(input.IsDown(XboxButton.A));
    }

    [Fact]
    public void SetButtonState_Pressed_IsDownReturnsTrue()
    {
        var input = new InputManager();
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        Assert.True(input.IsDown(XboxButton.A));
    }

    [Fact]
    public void IsJustPressed_BeforeUpdate_ReturnsTrue()
    {
        var input = new InputManager();
        input.SetButtonState(XboxButton.B, ButtonState.Pressed);
        Assert.True(input.IsJustPressed(XboxButton.B));
    }

    [Fact]
    public void IsJustPressed_AfterUpdate_ReturnsFalse_WhenStillPressed()
    {
        var input = new InputManager();
        input.SetButtonState(XboxButton.B, ButtonState.Pressed);
        input.Update(); // previous now = Pressed
        // current is still Pressed after Update (we didn't change it)
        Assert.False(input.IsJustPressed(XboxButton.B));
    }
}
