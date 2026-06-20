using Cards.Core.Models;
using Xunit;

namespace Cards.Tests;

public class DeckTests
{
    [Fact]
    public void NewDeck_Has52Cards()
    {
        var deck = new Deck();
        Assert.Equal(52, deck.Count);
    }

    [Fact]
    public void Draw_ReducesCount()
    {
        var deck = new Deck();
        deck.Draw();
        Assert.Equal(51, deck.Count);
    }

    [Fact]
    public void Draw_EmptyDeck_Throws()
    {
        var deck = new Deck();
        for (int i = 0; i < 52; i++)
            deck.Draw();

        Assert.Throws<InvalidOperationException>(() => deck.Draw());
    }

    [Fact]
    public void Shuffle_ChangesDeckOrder()
    {
        var deck1 = new Deck();
        var ordered = deck1.Cards.Select(c => c.ToString()).ToList();

        deck1.Shuffle(new Random(42));
        var shuffled = deck1.Cards.Select(c => c.ToString()).ToList();

        Assert.NotEqual(ordered, shuffled);
    }

    [Fact]
    public void Shuffle_PreservesAllCards()
    {
        var deck = new Deck();
        deck.Shuffle();
        Assert.Equal(52, deck.Count);
    }

    [Fact]
    public void Peek_DoesNotRemoveCard()
    {
        var deck = new Deck();
        var top = deck.Peek();
        Assert.Equal(52, deck.Count);
        Assert.Equal(top, deck.Peek());
    }
}
