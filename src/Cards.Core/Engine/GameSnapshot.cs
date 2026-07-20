using Cards.Core.Models;

namespace Cards.Core.Engine;

public sealed record GameSnapshot(
    string GameName,
    int PlayerCount,
    int CurrentPlayerIndex,
    int Round,
    bool IsComplete,
    int? WinnerIndex,
    IReadOnlyDictionary<int, int> Scores,
    IReadOnlyDictionary<int, int> TeamScores,
    IReadOnlyDictionary<int, int> PlayerTeams,
    IReadOnlyDictionary<int, IReadOnlyList<Card>> Hands,
    IReadOnlyDictionary<string, IReadOnlyList<Card>> Piles);
