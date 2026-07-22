namespace Cards.Core.Engine.Serialization;

public sealed record SavedGameState(
    string GameName,
    int PlayerCount,
    int CurrentPlayerIndex,
    int Round,
    bool IsComplete,
    int? WinnerIndex,
    IReadOnlyDictionary<int, int> Scores,
    IReadOnlyDictionary<int, int> TeamScores,
    IReadOnlyDictionary<int, int> PlayerTeams,
    IReadOnlyDictionary<int, IReadOnlyList<SerializableCard>> Hands,
    IReadOnlyDictionary<string, IReadOnlyList<SerializableCard>> Piles,
    int Seed,
    IReadOnlyList<SavedGameAction> History);
