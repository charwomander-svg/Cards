using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class CardTests
{
    [Fact]
    public void Card_ToString_ReturnsExpectedLabel()
    {
        var card = new Card(Suit.Spades, Rank.Ace);
        Assert.Equal("Ace of Spades", card.ToString());
    }

    [Fact]
    public void Card_Equality_SameSuitAndRank_AreEqual()
    {
        var a = new Card(Suit.Hearts, Rank.King);
        var b = new Card(Suit.Hearts, Rank.King);
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Card_Equality_DifferentCards_AreNotEqual()
    {
        var a = new Card(Suit.Hearts, Rank.King);
        var b = new Card(Suit.Clubs, Rank.King);
        Assert.NotEqual(a, b);
    }
}
