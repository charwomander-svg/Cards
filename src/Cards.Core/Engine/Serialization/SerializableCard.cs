using Cards.Core.Models;

namespace Cards.Core.Engine.Serialization;

public sealed record SerializableCard(Suit Suit, Rank Rank)
{
    public static SerializableCard FromCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        return new SerializableCard(card.Suit, card.Rank);
    }

    public Card ToCard() => new(Suit, Rank);
}
