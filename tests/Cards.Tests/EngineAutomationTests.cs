using Cards.Core.Engine;
using Cards.Core.Engine.Autoplay;
using Cards.Core.Engine.Hints;
using Cards.Core.Engine.Rules;
using Xunit;

namespace Cards.Tests;

public class EngineAutomationTests
{
    [Fact]
    public void ValidateSession_ReturnsValidForNewGame()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        Assert.True(engine.ValidateSession(session).IsValid);
    }

    [Fact]
    public void ValidateMove_ReturnsErrorForIllegalMove()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        var result = engine.ValidateMove(session, new MoveDescriptor("move", "waste", "foundation1"));

        Assert.False(result.IsValid);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == "move.illegal");
    }

    [Fact]
    public void HintProvider_ReturnsRankedUiHints()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        IReadOnlyList<Hint> hints = new HintProvider(engine).GetHints(session);

        Assert.NotEmpty(hints);
        Assert.All(hints, hint => Assert.NotEmpty(hint.Description.Label));
    }

    [Fact]
    public void AutoplayRunner_StepAppliesBestMove()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        MoveResult result = new AutoplayRunner(engine).Step(session);

        Assert.True(result.Succeeded);
        Assert.Single(session.History);
    }

    [Fact]
    public void AutoplayRunner_RunStopsAtMoveLimit()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        AutoplayResult result = new AutoplayRunner(engine).Run(session, 3);

        Assert.Equal(3, result.MovesAttempted);
        Assert.Equal(3, session.History.Count);
    }
}
