using Cards.Core;
using Cards.Core.Interfaces;
using Cards.Core.Models;
using Cards.Core.Tutorials;
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
    private readonly IReadOnlyList<GameTutorial> _tutorials = TutorialLibrary.All;
    private Deck _deck = new();
    private int _selectedTutorialIndex;
    private bool _wasPlayCardDown;
    private bool _wasOpenTutorialDown;
    private bool _wasCloseTutorialDown;
    private bool _wasStartDown;
    private bool _wasPreviousTutorialDown;
    private bool _wasNextTutorialDown;

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

            case GamePhase.Tutorial:
                HandleTutorial();
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
        if (WasPressed(XboxButton.Y, ref _wasOpenTutorialDown))
        {
            _state.Phase = GamePhase.Tutorial;
            RenderTutorial();
            return;
        }

        Hand currentHand = _hands[_state.CurrentPlayerIndex];
        _renderer.DrawText($"Player {_state.CurrentPlayerIndex + 1}'s turn — {currentHand.Count} cards", new Position(100, 100));
        _renderer.DrawText("Press Y for card game tutorials.", new Position(100, 150));

        if (WasPressed(XboxButton.A, ref _wasPlayCardDown) && !currentHand.IsEmpty)
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

    private void HandleTutorial()
    {
        if (WasPressed(XboxButton.B, ref _wasCloseTutorialDown) || WasPressed(XboxButton.Start, ref _wasStartDown))
        {
            _state.Phase = GamePhase.PlayerTurn;
            return;
        }

        if (WasPressed(XboxButton.DPadRight, ref _wasNextTutorialDown))
            NavigateNextTutorial();
        else if (WasPressed(XboxButton.DPadLeft, ref _wasPreviousTutorialDown))
            NavigatePreviousTutorial();

        RenderTutorial();
    }

    private void RenderTutorial()
    {
        GameTutorial tutorial = _tutorials[_selectedTutorialIndex];
        _renderer.DrawText("Tutorials: Left/Right choose game, B or Start returns", new Position(100, 80));
        _renderer.DrawText($"{_selectedTutorialIndex + 1}/{_tutorials.Count}: {tutorial.GameName}", new Position(100, 130));
        _renderer.DrawText($"Category: {tutorial.Category}", new Position(100, 180));
        _renderer.DrawText($"Goal: {tutorial.Objective}", new Position(100, 230));

        for (int i = 0; i < tutorial.Steps.Count; i++)
        {
            TutorialStep step = tutorial.Steps[i];
            _renderer.DrawText($"{i + 1}. {step.Title}: {step.Body}", new Position(100, 290 + (i * 70)));
        }

        if (!string.IsNullOrWhiteSpace(tutorial.Sources))
            _renderer.DrawText($"Sources: {tutorial.Sources}", new Position(100, 570));
    }

    private bool WasPressed(XboxButton button, ref bool wasDown)
    {
        bool isDown = _input.IsDown(button);
        bool pressed = isDown && !wasDown;
        wasDown = isDown;
        return pressed;
    }

    private void NavigateNextTutorial()
    {
        _selectedTutorialIndex++;
        if (_selectedTutorialIndex >= _tutorials.Count)
            _selectedTutorialIndex = 0;
    }

    private void NavigatePreviousTutorial()
    {
        _selectedTutorialIndex--;
        if (_selectedTutorialIndex < 0)
            _selectedTutorialIndex = _tutorials.Count - 1;
    }
}
