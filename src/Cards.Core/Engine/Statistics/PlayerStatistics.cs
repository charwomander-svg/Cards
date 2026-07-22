namespace Cards.Core.Engine.Statistics;

public sealed class PlayerStatistics
{
    public int CardsDrawn { get; private set; }
    public int CardsPlayed { get; private set; }
    public int CardsDiscarded { get; private set; }
    public int CardsCaptured { get; private set; }
    public int CardsCleared { get; private set; }
    public int MovesAttempted { get; private set; }
    public int MovesSucceeded { get; private set; }
    public int MovesFailed { get; private set; }
    public int TurnsTaken { get; private set; }
    public int ScoreGained { get; private set; }

    public double SuccessRate => MovesAttempted == 0 ? 0 : (double)MovesSucceeded / MovesAttempted;

    public void RecordMove(bool succeeded)
    {
        MovesAttempted++;
        if (succeeded)
            MovesSucceeded++;
        else
            MovesFailed++;
    }

    public void RecordDraw(int count) => CardsDrawn += count;
    public void RecordPlay(int count) => CardsPlayed += count;
    public void RecordDiscard(int count) => CardsDiscarded += count;
    public void RecordCapture(int count) => CardsCaptured += count;
    public void RecordClear(int count) => CardsCleared += count;
    public void RecordTurn() => TurnsTaken++;
    public void RecordScore(int score) => ScoreGained += score;
}
