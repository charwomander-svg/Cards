using Cards.Core.Models;

namespace Cards.Core.Engine;

/// <summary>Mutable stack-like pile used for stocks, waste piles, foundations, and tableaus.</summary>
public sealed class CardPile
{
    private readonly List<Card> _cards = new();
    private readonly HashSet<Card> _faceUpCards = new();

    public CardPile(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Pile name is required.", nameof(name));

        Name = name;
    }

    public string Name { get; }
    public int Count => _cards.Count;
    public bool IsEmpty => _cards.Count == 0;
    public Card Top => Peek();
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
    public IReadOnlyList<VisibleCard> VisibleCards => _cards.Select(card => new VisibleCard(card, _faceUpCards.Contains(card))).ToArray();

    public void Add(Card card, bool faceUp = true)
    {
        ArgumentNullException.ThrowIfNull(card);
        _cards.Add(card);
        if (faceUp)
            _faceUpCards.Add(card);
    }

    public void AddFaceDown(Card card) => Add(card, false);

    public void AddRange(IEnumerable<Card> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        foreach (Card card in cards)
            Add(card);
    }

    public Card Draw()
    {
        if (IsEmpty)
            throw new InvalidOperationException($"Cannot draw from empty pile '{Name}'.");

        Card card = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        _faceUpCards.Remove(card);
        return card;
    }

    public Card Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException($"Pile '{Name}' is empty.");

        return _cards[^1];
    }

    public IReadOnlyList<Card> DrawSequence(int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count), "Must draw at least one card.");
        if (count > _cards.Count)
            throw new InvalidOperationException($"Pile '{Name}' only has {_cards.Count} cards.");

        int start = _cards.Count - count;
        Card[] sequence = _cards.Skip(start).ToArray();
        _cards.RemoveRange(start, count);
        foreach (Card card in sequence)
            _faceUpCards.Remove(card);
        return sequence;
    }

    public bool Remove(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        _faceUpCards.Remove(card);
        return _cards.Remove(card);
    }

    public void SetFaceUp(Card card, bool isFaceUp)
    {
        ArgumentNullException.ThrowIfNull(card);
        if (!_cards.Contains(card))
            throw new InvalidOperationException($"Card '{card}' is not in pile '{Name}'.");

        if (isFaceUp)
            _faceUpCards.Add(card);
        else
            _faceUpCards.Remove(card);
    }

    public void FlipTop(bool isFaceUp = true)
    {
        SetFaceUp(Peek(), isFaceUp);
    }

    public bool IsFaceUp(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        return _faceUpCards.Contains(card);
    }

    public void Clear()
    {
        _cards.Clear();
        _faceUpCards.Clear();
    }
}
