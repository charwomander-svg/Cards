using Cards.Itch;
using Xunit;

namespace Cards.Tests;

public class ConsoleCardGameAppTests
{
    [Fact]
    public void Execute_SearchAndSelect_PrintsSelectedRules()
    {
        using var input = new StringReader(string.Empty);
        using var output = new StringWriter();
        var app = new ConsoleCardGameApp(input, output);

        app.Execute("search Klondike");
        app.Execute("select 1");
        app.Execute("rules");

        string text = output.ToString();
        Assert.Contains("Klondike", text);
        Assert.Contains("Set up the tableau", text);
    }

    [Fact]
    public void Execute_StartAndAuto_PrintsPlayableStatus()
    {
        using var input = new StringReader(string.Empty);
        using var output = new StringWriter();
        var app = new ConsoleCardGameApp(input, output);

        app.Execute("search Klondike");
        app.Execute("start");
        app.Execute("auto 2");

        string text = output.ToString();
        Assert.Contains("Started Klondike", text);
        Assert.Contains("Autoplay completed", text);
    }

    [Fact]
    public void Run_QuitCommand_ExitsCleanly()
    {
        using var input = new StringReader("quit");
        using var output = new StringWriter();
        var app = new ConsoleCardGameApp(input, output);

        app.Run();

        Assert.Contains("Itch.io Demo", output.ToString());
        Assert.Contains("Thanks for playing", output.ToString());
    }
}
