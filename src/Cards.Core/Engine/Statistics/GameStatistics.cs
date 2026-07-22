namespace Cards.Core.Engine.Statistics;

public sealed class GameStatistics
{
    private readonly Dictionary<int, PlayerStatistics> _players = new();
    private readonly Dictionary<string, PileStatistics> _piles = new(StringComparer.OrdinalIgnoreCase);

    public DateTimeOffset StartedAt { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? CompletedAt { get; private set; }
    public int MovesAttempted { get; private set; }
    public int MovesSucceeded { get; private set; }
    public int MovesFailed { get; private set; }
    public int TotalCardsDrawn { get; private set; }
    public int TotalCardsPlayed { get; private set; }
    public int TotalCardsDiscarded { get; private set; }
    public int TotalCardsCaptured { get; private set; }
    public int TotalCardsCleared { get; private set; }
    public int TotalScoreGained { get; private set; }
    public int TurnsTaken { get; private set; }
    public int RoundsCompleted { get; private set; }
    public int FailedMoveStreak { get; private set; }
    public int LongestFailedMoveStreak { get; private set; }
    public IReadOnlyDictionary<int, PlayerStatistics> Players => _players;
    public IReadOnlyDictionary<string, PileStatistics> Piles => _piles;
    public TimeSpan Elapsed => (CompletedAt ?? DateTimeOffset.UtcNow) - StartedAt;
    public double SuccessRate => MovesAttempted == 0 ? 0 : (double)MovesSucceeded / MovesAttempted;

    public PlayerStatistics GetPlayer(int playerIndex)
    {
        if (playerIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(playerIndex), "Player index cannot be negative.");

        if (!_players.TryGetValue(playerIndex, out PlayerStatistics? statistics))
        {
            statistics = new PlayerStatistics();
            _players[playerIndex] = statistics;
        }

        return statistics;
    }

    public PileStatistics GetPile(string pileName)
    {
        if (!_piles.TryGetValue(pileName, out PileStatistics? statistics))
        {
            statistics = new PileStatistics(pileName);
            _piles[pileName] = statistics;
        }

        return statistics;
    }

    public void RecordMove(int playerIndex, bool succeeded)
    {
        MovesAttempted++;
        GetPlayer(playerIndex).RecordMove(succeeded);
        if (succeeded)
        {
            MovesSucceeded++;
            FailedMoveStreak = 0;
        }
        else
        {
            MovesFailed++;
            FailedMoveStreak++;
            LongestFailedMoveStreak = Math.Max(LongestFailedMoveStreak, FailedMoveStreak);
        }
    }

    public void RecordDraw(int playerIndex, int count)
    {
        TotalCardsDrawn += count;
        GetPlayer(playerIndex).RecordDraw(count);
    }

    public void RecordPlay(int playerIndex, int count)
    {
        TotalCardsPlayed += count;
        GetPlayer(playerIndex).RecordPlay(count);
    }

    public void RecordDiscard(int playerIndex, int count)
    {
        TotalCardsDiscarded += count;
        GetPlayer(playerIndex).RecordDiscard(count);
    }

    public void RecordCapture(int playerIndex, int count)
    {
        TotalCardsCaptured += count;
        GetPlayer(playerIndex).RecordCapture(count);
    }

    public void RecordClear(int playerIndex, int count)
    {
        TotalCardsCleared += count;
        GetPlayer(playerIndex).RecordClear(count);
    }

    public void RecordScore(int playerIndex, int score)
    {
        TotalScoreGained += score;
        GetPlayer(playerIndex).RecordScore(score);
    }

    public void RecordTurn(int playerIndex)
    {
        TurnsTaken++;
        GetPlayer(playerIndex).RecordTurn();
    }

    public void RecordRoundCompleted()
    {
        RoundsCompleted++;
    }

    public void RecordPileAdded(string pileName, int count, int currentCount)
    {
        GetPile(pileName).RecordAdded(count, currentCount);
    }

    public void RecordPileRemoved(string pileName, int count)
    {
        GetPile(pileName).RecordRemoved(count);
    }

    public void Complete()
    {
        CompletedAt ??= DateTimeOffset.UtcNow;
    }
}
