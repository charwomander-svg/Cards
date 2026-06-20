namespace Cards.Core.Models;

/// <summary>
/// Represents a standard 52-card deck. Supports shuffling and drawing cards.
/// </summary>
public sealed class Deck
{
    private readonly List<Card> _cards;

    public int Count => _cards.Count;
    public bool IsEmpty => _cards.Count == 0;

    /// <summary>Creates a freshly ordered 52-card deck.</summary>
    public Deck()
    {
        _cards = new List<Card>(52);
        foreach (Suit suit in Enum.GetValues<Suit>())
            foreach (Rank rank in Enum.GetValues<Rank>())
                _cards.Add(new Card(suit, rank));
    }

    /// <summary>Shuffles the deck using Fisher-Yates algorithm.</summary>
    public void Shuffle(Random? random = null)
    {
        var rng = random ?? Random.Shared;
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    /// <summary>Draws the top card from the deck.</summary>
    /// <exception cref="InvalidOperationException">Thrown when the deck is empty.</exception>
    public Card Draw()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cannot draw from an empty deck.");

        Card card = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    /// <summary>Peeks at the top card without removing it.</summary>
    public Card Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("The deck is empty.");
        return _cards[^1];
    }

    /// <summary>Returns a read-only view of the remaining cards.</summary>
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
}
