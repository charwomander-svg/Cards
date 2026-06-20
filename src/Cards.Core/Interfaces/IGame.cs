namespace Cards.Core.Interfaces;

/// <summary>
/// Defines the contract for any game managed by the Cards engine.
/// </summary>
public interface IGame
{
    /// <summary>Human-readable name of the game.</summary>
    string Name { get; }

    /// <summary>Initializes the game to its starting state.</summary>
    void Initialize();

    /// <summary>Advances the game by one logical tick.</summary>
    void Update();

    /// <summary>Signals the game that it should clean up and exit.</summary>
    void Shutdown();
}
