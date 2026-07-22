using Cards.Core.Models;

namespace Cards.Core.Engine.Scoring;

public static class ScoreCalculator
{
    public static int CardValue(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        return card.Rank switch
        {
            Rank.Ace => 1,
            Rank.Jack or Rank.Queen or Rank.King => 10,
            _ => (int)card.Rank
        };
    }

    public static int CalculateHandValue(IEnumerable<Card> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        return cards.Sum(CardValue);
    }

    public static int CalculateScore(CardGameSession session, int playerIndex, ScorePolicy policy)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (playerIndex < 0 || playerIndex >= session.PlayerCount)
            throw new ArgumentOutOfRangeException(nameof(playerIndex), "Player index is outside the session player range.");

        return policy switch
        {
            ScorePolicy.RankValue => CalculateHandValue(session.Hands[playerIndex].Cards),
            ScorePolicy.TrickCount => session.GetPile("trick").Count,
            ScorePolicy.CaptureCount => session.Piles.TryGetValue("captured", out CardPile? captured) ? captured.Count : 0,
            ScorePolicy.LowHandPenalty => -CalculateHandValue(session.Hands[playerIndex].Cards),
            ScorePolicy.SolitaireCompletion => session.Piles.Values.Where(pile => pile.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase)).Sum(pile => pile.Count),
            _ => throw new NotSupportedException($"Score policy '{policy}' is not supported.")
        };
    }
}
