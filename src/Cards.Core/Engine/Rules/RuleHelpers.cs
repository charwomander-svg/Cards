using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

internal static class RuleHelpers
{
    public static bool IsOppositeColor(Card first, Card second) => IsRed(first) != IsRed(second);

    public static bool IsRed(Card card) => card.Suit is Suit.Diamonds or Suit.Hearts;

    public static bool IsOneRankLower(Card candidate, Card target) => (int)candidate.Rank + 1 == (int)target.Rank;

    public static bool IsOneRankHigher(Card candidate, Card target) => (int)candidate.Rank == (int)target.Rank + 1;
}
