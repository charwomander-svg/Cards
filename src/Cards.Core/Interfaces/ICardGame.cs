using Cards.Core.Models;

namespace Cards.Core.Interfaces;

/// <summary>
/// Extends <see cref="IGame"/> with card-game-specific operations.
/// </summary>
public interface ICardGame : IGame
{
    /// <summary>Number of players participating in the game.</summary>
    int PlayerCount { get; }

    /// <summary>Deals the initial cards to all players.</summary>
    void Deal();

    /// <summary>Determines and returns the winner index, or -1 if the game is still in progress.</summary>
    int GetWinner();
}
