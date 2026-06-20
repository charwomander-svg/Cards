using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class HandTests
{
    [Fact]
    public void NewHand_IsEmpty()
    {
        var hand = new Hand();
        Assert.True(hand.IsEmpty);
        Assert.Equal(0, hand.Count);
    }

    [Fact]
    public void AddCard_IncreasesCount()
    {
        var hand = new Hand();
        hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
        Assert.Equal(1, hand.Count);
    }

    [Fact]
    public void PlayCard_RemovesCard_ReturnsTrue()
    {
        var hand = new Hand();
        var card = new Card(Suit.Diamonds, Rank.Seven);
        hand.AddCard(card);
        bool removed = hand.PlayCard(card);
        Assert.True(removed);
        Assert.True(hand.IsEmpty);
    }

    [Fact]
    public void PlayCard_CardNotInHand_ReturnsFalse()
    {
        var hand = new Hand();
        var card = new Card(Suit.Clubs, Rank.Two);
        Assert.False(hand.PlayCard(card));
    }

    [Fact]
    public void Clear_RemovesAllCards()
    {
        var hand = new Hand();
        hand.AddCard(new Card(Suit.Spades, Rank.Ten));
        hand.AddCard(new Card(Suit.Hearts, Rank.Five));
        hand.Clear();
        Assert.True(hand.IsEmpty);
    }

    [Fact]
    public void AddCard_Null_Throws()
    {
        var hand = new Hand();
        Assert.Throws<ArgumentNullException>(() => hand.AddCard(null!));
    }
}
