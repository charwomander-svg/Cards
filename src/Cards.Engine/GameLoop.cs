using Cards.Core.Interfaces;

namespace Cards.Engine;

/// <summary>
/// Drives the main update loop for a game.
/// Decouples timing logic from game-specific code.
/// </summary>
public sealed class GameLoop
{
    private readonly IGame _game;
    private bool _running;

    public GameLoop(IGame game)
    {
        ArgumentNullException.ThrowIfNull(game);
        _game = game;
    }

    /// <summary>
    /// Initializes the game and begins ticking until <see cref="Stop"/> is called.
    /// </summary>
    public void Run()
    {
        _game.Initialize();
        _running = true;

        while (_running)
        {
            _game.Update();
        }

        _game.Shutdown();
    }

    /// <summary>Signals the loop to exit after the current tick completes.</summary>
    public void Stop() => _running = false;
}
