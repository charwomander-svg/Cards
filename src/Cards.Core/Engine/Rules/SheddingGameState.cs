using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class SheddingGameState
{
    public int Direction { get; private set; } = 1;
    public Suit? ActiveSuit { get; private set; }
    public int PendingDrawCount { get; private set; }

    public void Reverse() => Direction *= -1;
    public void SetActiveSuit(Suit? suit) => ActiveSuit = suit;
    public void AddDrawPenalty(int count) => PendingDrawCount += count;
    public int ConsumeDrawPenalty()
    {
        int count = PendingDrawCount;
        PendingDrawCount = 0;
        return count;
    }
}
