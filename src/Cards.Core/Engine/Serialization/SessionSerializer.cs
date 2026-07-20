using System.Text.Json;

namespace Cards.Core.Engine.Serialization;

public static class SessionSerializer
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public static SavedGameState Save(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        GameSnapshot snapshot = session.CreateSnapshot();

        return new SavedGameState(
            snapshot.GameName,
            snapshot.PlayerCount,
            snapshot.CurrentPlayerIndex,
            snapshot.Round,
            snapshot.IsComplete,
            snapshot.WinnerIndex,
            snapshot.Scores,
            snapshot.TeamScores,
            snapshot.PlayerTeams,
            snapshot.Hands.ToDictionary(
                pair => pair.Key,
                pair => (IReadOnlyList<SerializableCard>)pair.Value.Select(SerializableCard.FromCard).ToArray()),
            snapshot.Piles.ToDictionary(
                pair => pair.Key,
                pair => (IReadOnlyList<SerializableCard>)pair.Value.Select(SerializableCard.FromCard).ToArray(),
                StringComparer.OrdinalIgnoreCase),
        session.Seed,
        session.History.Select(action => new SavedGameAction(
            action.Sequence,
            action.PlayerIndex,
            action.Move,
            action.Result.Succeeded,
            action.Result.Message,
            action.Timestamp)).ToArray());
    }

    public static string ToJson(CardGameSession session) => JsonSerializer.Serialize(Save(session), Options);

    public static SavedGameState FromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("Saved game JSON is required.", nameof(json));

        return JsonSerializer.Deserialize<SavedGameState>(json, Options)
            ?? throw new InvalidOperationException("Saved game JSON did not contain a valid state.");
    }

    public static CardGameSession Restore(SavedGameState saved)
    {
        ArgumentNullException.ThrowIfNull(saved);
        var session = new CardGameSession(saved.GameName, saved.PlayerCount);
        session.Seed = saved.Seed;
        session.RestoreState(
            saved.CurrentPlayerIndex,
            saved.Round,
            saved.IsComplete,
            saved.Scores,
            saved.TeamScores,
            saved.PlayerTeams,
            saved.Hands.ToDictionary(
                pair => pair.Key,
                pair => (IReadOnlyList<Models.Card>)pair.Value.Select(card => card.ToCard()).ToArray()),
            saved.Piles.ToDictionary(
                pair => pair.Key,
                pair => (IReadOnlyList<Models.Card>)pair.Value.Select(card => card.ToCard()).ToArray(),
                StringComparer.OrdinalIgnoreCase));
        return session;
    }

    public static CardGameSession RestoreJson(string json) => Restore(FromJson(json));
}
