namespace Cards.Core;

/// <summary>Represents the high-level state of the running game.</summary>
public enum GamePhase
{
    MainMenu,
    Dealing,
    PlayerTurn,
    AiTurn,
    RoundEnd,
    GameOver
}

/// <summary>
/// Holds shared, mutable state that is passed between game systems each frame.
/// </summary>
public sealed class GameState
{
    public GamePhase Phase { get; set; } = GamePhase.MainMenu;

    /// <summary>Zero-based index of the player whose turn it currently is.</summary>
    public int CurrentPlayerIndex { get; set; } = 0;

    /// <summary>Current round number (1-based).</summary>
    public int Round { get; set; } = 1;

    /// <summary>Score keyed by player index.</summary>
    public Dictionary<int, int> Scores { get; } = new();

    /// <summary>Resets the state back to a new-game baseline.</summary>
    public void Reset()
    {
        Phase = GamePhase.MainMenu;
        CurrentPlayerIndex = 0;
        Round = 1;
        Scores.Clear();
    }
}
