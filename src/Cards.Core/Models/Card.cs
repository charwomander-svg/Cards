namespace Cards.Core.Models;

/// <summary>Represents a single playing card with a suit and rank.</summary>
public sealed class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    /// <summary>Returns a human-readable label such as "Ace of Spades".</summary>
    public override string ToString() => $"{Rank} of {Suit}";

    public override bool Equals(object? obj) =>
        obj is Card other && Suit == other.Suit && Rank == other.Rank;

    public override int GetHashCode() => HashCode.Combine(Suit, Rank);
}
