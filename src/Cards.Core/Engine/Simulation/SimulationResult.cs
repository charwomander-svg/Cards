namespace Cards.Core.Engine.Simulation;

public sealed record SimulationResult(
    string GameName,
    int MovesAttempted,
    int MovesSucceeded,
    bool Completed,
    int? WinnerIndex,
    Statistics.StatisticsSnapshot Statistics);
