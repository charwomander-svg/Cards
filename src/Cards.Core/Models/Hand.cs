namespace Cards.Core.Models;

/// <summary>Represents a player's hand of cards.</summary>
public sealed class Hand
{
    private readonly List<Card> _cards = new();

    public int Count => _cards.Count;
    public bool IsEmpty => _cards.Count == 0;

    /// <summary>Adds a card to the hand.</summary>
    public void AddCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        _cards.Add(card);
    }

    /// <summary>Removes and returns the specified card from the hand.</summary>
    public bool PlayCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        return _cards.Remove(card);
    }

    /// <summary>Removes all cards from the hand.</summary>
    public void Clear() => _cards.Clear();

    /// <summary>Returns a read-only view of the cards currently in the hand.</summary>
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
}
