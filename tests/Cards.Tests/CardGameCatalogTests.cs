using Cards.Core.Data;
using Xunit;

namespace Cards.Tests;

public class CardGameCatalogTests
{
    [Fact]
    public void StyleGroups_ContainMajorGameFamilies()
    {
        Assert.Contains(CardGameCatalog.StyleGroups, group => group.Name == "Builder Solitaire");
        Assert.Contains(CardGameCatalog.StyleGroups, group => group.Name == "Trick-Taking");
        Assert.Contains(CardGameCatalog.StyleGroups, group => group.Name == "Rummy and Meld-Building");
        Assert.Contains(CardGameCatalog.StyleGroups, group => group.Name == "Banking, Gambling, and Poker");
    }

    [Theory]
    [InlineData("Klondike", "Builder Solitaire")]
    [InlineData("Spider", "Spider-Style Solitaire")]
    [InlineData("Pyramid", "Pairing and Clearing Solitaire")]
    [InlineData("Bridge", "Trick-Taking")]
    [InlineData("Canasta", "Rummy and Meld-Building")]
    [InlineData("Crazy Eights", "Shedding and Matching PvP")]
    [InlineData("Scopa", "Fishing and Capture")]
    [InlineData("Texas Hold'em", "Banking, Gambling, and Poker")]
    public void StyleGroupByGame_MapsKnownGamesToStyles(string gameName, string expectedStyle)
    {
        Assert.True(CardGameCatalog.StyleGroupByGame.TryGetValue(gameName, out CardGameStyleGroup? group));
        Assert.Equal(expectedStyle, group.Name);
    }
}
