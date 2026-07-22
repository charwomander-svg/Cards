namespace Cards.Core.Engine.Statistics;

public sealed class PileStatistics
{
    public PileStatistics(string pileName)
    {
        if (string.IsNullOrWhiteSpace(pileName))
            throw new ArgumentException("Pile name is required.", nameof(pileName));

        PileName = pileName;
    }

    public string PileName { get; }
    public int CardsAdded { get; private set; }
    public int CardsRemoved { get; private set; }
    public int PeakCount { get; private set; }

    public void RecordAdded(int count, int currentCount)
    {
        CardsAdded += count;
        PeakCount = Math.Max(PeakCount, currentCount);
    }

    public void RecordRemoved(int count)
    {
        CardsRemoved += count;
    }
}
