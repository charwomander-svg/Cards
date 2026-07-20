namespace Cards.Core.Engine.Rounds;

public sealed record RoundSummary(
    string GameName,
    int Round,
    bool IsComplete,
    int? WinnerIndex,
    IReadOnlyDictionary<int, int> Scores,
    IReadOnlyDictionary<int, int> TeamScores,
    Endings.EndConditionResult EndCondition,
    Statistics.StatisticsSnapshot Statistics);
