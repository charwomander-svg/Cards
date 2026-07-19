using Cards.Core.Tutorials;
using Xunit;

namespace Cards.Tests;

public class TutorialLibraryTests
{
    [Fact]
    public void All_ContainsCompiledRulesForMultipleGames()
    {
        Assert.True(TutorialLibrary.All.Count >= 6);
        Assert.Contains(TutorialLibrary.All, tutorial => tutorial.GameName == "War");
        Assert.Contains(TutorialLibrary.All, tutorial => tutorial.GameName == "Blackjack");
        Assert.All(TutorialLibrary.All, tutorial => Assert.True(tutorial.Steps.Count >= 4));
    }

    [Fact]
    public void GetByName_IsCaseInsensitive()
    {
        var tutorial = TutorialLibrary.GetByName("go fish");

        Assert.Equal("Go Fish", tutorial.GameName);
    }
}
