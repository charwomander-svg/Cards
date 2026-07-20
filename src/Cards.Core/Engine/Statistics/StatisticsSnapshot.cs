namespace Cards.Core.Engine.Statistics;

public sealed record StatisticsSnapshot(
    DateTimeOffset StartedAt,
    DateTimeOffset? CompletedAt,
    TimeSpan Elapsed,
    int MovesAttempted,
    int MovesSucceeded,
    int MovesFailed,
    double SuccessRate,
    int TotalCardsDrawn,
    int TotalCardsPlayed,
    int TotalCardsDiscarded,
    int TotalCardsCaptured,
    int TotalCardsCleared,
    int TotalScoreGained,
    int TurnsTaken,
    int RoundsCompleted,
    int LongestFailedMoveStreak,
    IReadOnlyDictionary<int, PlayerStatisticsSnapshot> Players,
    IReadOnlyDictionary<string, PileStatisticsSnapshot> Piles);

public sealed record PlayerStatisticsSnapshot(
    int CardsDrawn,
    int CardsPlayed,
    int CardsDiscarded,
    int CardsCaptured,
    int CardsCleared,
    int MovesAttempted,
    int MovesSucceeded,
    int MovesFailed,
    double SuccessRate,
    int TurnsTaken,
    int ScoreGained);

public sealed record PileStatisticsSnapshot(string PileName, int CardsAdded, int CardsRemoved, int PeakCount);
