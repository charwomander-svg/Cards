using Cards.Core;
using Cards.Core.Interfaces;
using Cards.Core.Models;
using Cards.Engine;

namespace Cards.Xbox;

/// <summary>
/// Concrete card game implementation used as the default launch game.
/// Wires together the Core models and the Engine services.
/// </summary>
public sealed class CardCollectionGame : ICardGame
{
    private readonly GameState _state = new();
    private readonly InputManager _input;
    private readonly RenderManager _renderer;
    private readonly List<Hand> _hands = new();
    private Deck _deck = new();

    public string Name => "The Ultimate Card Game Collection";
    public int PlayerCount { get; }

    public CardCollectionGame(int playerCount, InputManager input, RenderManager renderer)
    {
        if (playerCount < 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Must have at least one player.");

        PlayerCount = playerCount;
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    }

    public void Initialize()
    {
        _state.Reset();
        _deck = new Deck();
        _deck.Shuffle();

        _hands.Clear();
        for (int i = 0; i < PlayerCount; i++)
            _hands.Add(new Hand());

        _state.Phase = GamePhase.Dealing;
    }

    public void Deal()
    {
        const int cardsPerPlayer = 7;
        for (int i = 0; i < cardsPerPlayer; i++)
            foreach (Hand hand in _hands)
                if (!_deck.IsEmpty)
                    hand.AddCard(_deck.Draw());

        _state.Phase = GamePhase.PlayerTurn;
    }

    public void Update()
    {
        _input.Update();
        _renderer.BeginFrame();

        switch (_state.Phase)
        {
            case GamePhase.Dealing:
                Deal();
                break;

            case GamePhase.PlayerTurn:
                HandlePlayerTurn();
                break;

            case GamePhase.GameOver:
                _renderer.DrawText($"Game Over! Winner: Player {GetWinner() + 1}", new Position(760, 540));
                break;
        }

        _renderer.EndFrame();
    }

    public void Shutdown()
    {
        _hands.Clear();
    }

    public int GetWinner()
    {
        if (_state.Phase != GamePhase.GameOver)
            return -1;

        int winner = 0;
        int best = _state.Scores.GetValueOrDefault(0, 0);
        for (int i = 1; i < PlayerCount; i++)
        {
            int score = _state.Scores.GetValueOrDefault(i, 0);
            if (score > best)
            {
                best = score;
                winner = i;
            }
        }
        return winner;
    }

    private void HandlePlayerTurn()
    {
        Hand currentHand = _hands[_state.CurrentPlayerIndex];
        _renderer.DrawText($"Player {_state.CurrentPlayerIndex + 1}'s turn — {currentHand.Count} cards", new Position(100, 100));

        if (_input.IsJustPressed(XboxButton.A) && !currentHand.IsEmpty)
        {
            Card played = currentHand.Cards[0];
            currentHand.PlayCard(played);
            _renderer.DrawText($"Played: {played}", new Position(100, 200));

            _state.Scores[_state.CurrentPlayerIndex] =
                _state.Scores.GetValueOrDefault(_state.CurrentPlayerIndex, 0) + (int)played.Rank;

            AdvanceTurn();
        }
    }

    private void AdvanceTurn()
    {
        if (_hands.All(h => h.IsEmpty))
        {
            _state.Phase = GamePhase.GameOver;
            return;
        }

        _state.CurrentPlayerIndex = (_state.CurrentPlayerIndex + 1) % PlayerCount;
        if (_state.CurrentPlayerIndex == 0)
            _state.Round++;
    }
}
