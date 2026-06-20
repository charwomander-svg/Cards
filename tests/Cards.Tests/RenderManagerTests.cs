using Cards.Engine;
using Xunit;

namespace Cards.Tests;

public class RenderManagerTests
{
    [Fact]
    public void BeginFrame_ClearsDrawLog()
    {
        var renderer = new RenderManager();
        renderer.DrawSprite("card_ace_spades", new Position(100, 200));
        renderer.BeginFrame();
        Assert.Empty(renderer.GetFrameDrawLog());
    }

    [Fact]
    public void DrawSprite_AddsEntryToLog()
    {
        var renderer = new RenderManager();
        renderer.DrawSprite("card_king_hearts", new Position(0, 0));
        Assert.Single(renderer.GetFrameDrawLog());
    }

    [Fact]
    public void DrawText_AddsEntryToLog()
    {
        var renderer = new RenderManager();
        renderer.DrawText("Hello Xbox", new Position(50, 50));
        var log = renderer.GetFrameDrawLog();
        Assert.Single(log);
        Assert.Contains("Hello Xbox", log[0]);
    }

    [Fact]
    public void DefaultResolution_Is1920x1080()
    {
        var renderer = new RenderManager();
        Assert.Equal(1920, renderer.Width);
        Assert.Equal(1080, renderer.Height);
    }
}
