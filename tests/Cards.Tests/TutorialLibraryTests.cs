using Cards.Core.Tutorials;
using Xunit;

namespace Cards.Tests;

public class TutorialLibraryTests
{
    [Fact]
    public void All_ContainsCompiledRulesForHundredsOfGames()
    {
        Assert.Equal(408, TutorialLibrary.All.Count);
        Assert.Contains(TutorialLibrary.All, tutorial => tutorial.GameName.StartsWith("Klondike"));
        Assert.Contains(TutorialLibrary.All, tutorial => tutorial.GameName == "Bridge (Contract Bridge)");
        Assert.Contains(TutorialLibrary.All, tutorial => tutorial.GameName == "Scopa");
        Assert.All(TutorialLibrary.All, tutorial => Assert.True(tutorial.Steps.Count >= 3));
        Assert.All(TutorialLibrary.All, tutorial => Assert.False(string.IsNullOrWhiteSpace(tutorial.Category)));
        Assert.All(TutorialLibrary.All, tutorial => Assert.False(string.IsNullOrWhiteSpace(tutorial.Sources)));
    }

    [Fact]
    public void GetByName_IsCaseInsensitive()
    {
        var tutorial = TutorialLibrary.GetByName("go fish");

        Assert.Equal("Go Fish", tutorial.GameName);
    }
}
