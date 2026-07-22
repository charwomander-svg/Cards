using Cards.Core.Data;
using Cards.Core.Interfaces;
using Cards.Engine;

namespace Cards.Xbox;

/// <summary>
/// Concrete card game implementation used as the default launch game.
/// Wires together the Core models and the Engine services.
/// </summary>
public sealed class CardCollectionGame : ICardGame
{
    private readonly InputManager _input;
    private readonly RenderManager _renderer;
    private readonly CollectionUiViewModel _viewModel;

    public string Name => "The Ultimate Card Game Collection";
    public int PlayerCount { get; private set; }

    public CardCollectionGame(int playerCount, InputManager input, RenderManager renderer, CollectionUiViewModel? viewModel = null)
    {
        if (playerCount < 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Must have at least one player.");

        PlayerCount = playerCount;
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        _viewModel = viewModel ?? new CollectionUiViewModel();
    }

    public void Initialize() => _viewModel.ApplyFilter(string.Empty);

    public void Deal() => StartSelectedGame();

    public void Update()
    {
        HandleInput();

        _renderer.BeginFrame();
        DrawShell();
        _renderer.EndFrame();
        _input.CompleteFrame();
    }

    public void Shutdown() { }

    public int GetWinner()
    {
        return _viewModel.Session?.WinnerIndex ?? -1;
    }

    public CollectionUiViewModel ViewModel => _viewModel;

    private void HandleInput()
    {
        if (_input.IsJustPressed(XboxButton.Y))
            _viewModel.SetMode(CollectionUiMode.Tutorial);
        if (_input.IsJustPressed(XboxButton.LeftShoulder))
            _viewModel.MoveCategory(-1);
        if (_input.IsJustPressed(XboxButton.RightShoulder))
            _viewModel.MoveCategory(1);
        if (_input.IsJustPressed(XboxButton.LeftShoulder) && _input.IsDown(XboxButton.X))
            _viewModel.MoveSearchPreset(-1);
        if (_input.IsJustPressed(XboxButton.RightShoulder) && _input.IsDown(XboxButton.X))
            _viewModel.MoveSearchPreset(1);
        if (_input.IsJustPressed(XboxButton.Back) && _viewModel.Mode == CollectionUiMode.Library)
            _viewModel.ToggleFavorite();
        if (_input.IsJustPressed(XboxButton.B))
            _viewModel.SetMode(CollectionUiMode.Library);

        if (_viewModel.Mode == CollectionUiMode.Library && _input.IsJustPressed(XboxButton.DPadDown))
            _viewModel.MoveSelection(1);
        if (_viewModel.Mode == CollectionUiMode.Library && _input.IsJustPressed(XboxButton.DPadUp))
            _viewModel.MoveSelection(-1);
        if (_viewModel.Mode == CollectionUiMode.Library && _input.IsJustPressed(XboxButton.DPadRight))
            _viewModel.AdjustPlayerCount(1);
        if (_viewModel.Mode == CollectionUiMode.Library && _input.IsJustPressed(XboxButton.DPadLeft))
            _viewModel.AdjustPlayerCount(-1);

        if (_viewModel.Mode == CollectionUiMode.Table && _input.IsJustPressed(XboxButton.DPadRight))
            _viewModel.MoveActionSelection(1);
        if (_viewModel.Mode == CollectionUiMode.Table && _input.IsJustPressed(XboxButton.DPadLeft))
            _viewModel.MoveActionSelection(-1);
        if (_viewModel.Mode == CollectionUiMode.Tutorial && _input.IsJustPressed(XboxButton.DPadRight))
            _viewModel.MoveTutorialStep(1);
        if (_viewModel.Mode == CollectionUiMode.Tutorial && _input.IsJustPressed(XboxButton.DPadLeft))
            _viewModel.MoveTutorialStep(-1);

        if (_input.IsJustPressed(XboxButton.A))
            StartSelectedGame();
        if (_input.IsJustPressed(XboxButton.X))
            _viewModel.ApplySelectedAction(_viewModel.SelectedActionIndex);
        if (_input.IsJustPressed(XboxButton.RightShoulder) && _viewModel.Mode == CollectionUiMode.Table)
            _viewModel.RunAutoplayStep();
        if (_input.IsJustPressed(XboxButton.LeftShoulder) && _viewModel.Mode == CollectionUiMode.Table)
            _viewModel.RunAutoplay();
        if (_input.IsJustPressed(XboxButton.Start))
            _viewModel.SaveSession();
        if (_input.IsJustPressed(XboxButton.Back) && _viewModel.Mode != CollectionUiMode.Library)
            _viewModel.LoadSavedSession();
    }

    private void StartSelectedGame()
    {
        var result = _viewModel.StartSelectedGame(_viewModel.ConfiguredPlayerCount);
        if (result.Succeeded)
            PlayerCount = _viewModel.ConfiguredPlayerCount;
    }

    public void ApplyFilter(string filter) => _viewModel.ApplyFilter(filter);

    private void DrawShell()
    {
        _renderer.DrawText(Name, new Position(64, 40));
        _renderer.DrawText("Library: Up/Down games, Left/Right players, LB/RB category, X+LB/RB search, Back favorite | Table: Left/Right actions | A start | X play", new Position(64, 80));
        _renderer.DrawText(_viewModel.Status, new Position(64, 116));
        _renderer.DrawText($"Mode: {_viewModel.Mode}   Players: {_viewModel.ConfiguredPlayerCount}   Category: {_viewModel.ActiveCategoryName}   Search: {_viewModel.ActiveSearchPreset}", new Position(64, 140));

        DrawGameList();
        DrawTutorial();
        if (_viewModel.Mode == CollectionUiMode.Table)
            DrawTable();
    }

    private void DrawGameList()
    {
        _renderer.DrawText($"Library: {_viewModel.VisibleGames.Count} games", new Position(64, 170));
        int start = Math.Max(0, _viewModel.SelectedGameIndex - 5);
        int end = Math.Min(_viewModel.VisibleGames.Count, start + 11);
        for (int i = start; i < end; i++)
        {
            CardGameRuleSet game = _viewModel.VisibleGames[i];
            string marker = i == _viewModel.SelectedGameIndex ? ">" : " ";
            string favorite = _viewModel.IsFavorite(game.Name) ? "*" : " ";
            _renderer.DrawText($"{marker}{favorite} {game.Name} ({game.Category}, {game.Players})", new Position(64, 210 + ((i - start) * 32)));
        }

        if (_viewModel.RecentGames.Count > 0)
            _renderer.DrawText($"Recent: {string.Join(", ", _viewModel.RecentGames)}", new Position(64, 580));
    }

    private void DrawTutorial()
    {
        CardGameRuleSet selected = _viewModel.SelectedGame;
        _renderer.DrawText(selected.Objective, new Position(640, 160));
        for (int i = 0; i < Math.Min(4, selected.HowToPlay.Count); i++)
        {
            string marker = i == _viewModel.TutorialStepIndex ? ">" : " ";
            _renderer.DrawText($"{marker} {i + 1}. {selected.HowToPlay[i]}", new Position(640, 210 + (i * 34)));
        }
    }

    private void DrawTable()
    {
        var snapshot = _viewModel.Snapshot();
        if (snapshot is null)
            return;

        _renderer.DrawText($"{snapshot.GameName} - Player {snapshot.CurrentPlayerIndex + 1}'s turn - Round {snapshot.Round}", new Position(640, 390));
        SessionUiSummary? summary = _viewModel.BuildSessionSummary();
        if (summary is not null)
            _renderer.DrawText($"Summary: {summary.LegalMoveCount} moves, {summary.HandCount} hands, {summary.PileCount} piles, leader P{(summary.LeadingPlayer ?? 0) + 1} ({summary.LeadingScore}), complete: {summary.IsComplete}", new Position(640, 410));
        string validation = _viewModel.Validation.IsValid ? "valid" : $"{_viewModel.Validation.Diagnostics.Count} issue(s)";
        string autoplay = _viewModel.LastAutoplayResult is null ? "not run" : $"{_viewModel.LastAutoplayResult.MovesSucceeded}/{_viewModel.LastAutoplayResult.MovesAttempted}";
        _renderer.DrawText($"Validation: {validation}   Autoplay: {autoplay}   LB: auto 5   RB: auto 1", new Position(640, 430));
        int row = 0;
        foreach (var hand in snapshot.Hands.OrderBy(pair => pair.Key))
            _renderer.DrawText($"P{hand.Key + 1} hand: {FormatCards(hand.Value, 6)}", new Position(640, 470 + (row++ * 30)));

        row = 0;
        foreach (var pile in snapshot.Piles.OrderBy(pair => pair.Key).Take(12))
            _renderer.DrawText($"{pile.Key}: {pile.Value.Count} {FormatCards(pile.Value.TakeLast(3).ToArray(), 3)}", new Position(940, 430 + (row++ * 30)));

        row = 0;
        foreach (var score in snapshot.Scores.OrderBy(pair => pair.Key))
            _renderer.DrawText($"P{score.Key + 1} score: {score.Value}", new Position(640, 620 + (row++ * 28)));

        _renderer.DrawText("Actions", new Position(1240, 390));
        for (int i = 0; i < Math.Min(6, _viewModel.Actions.Count); i++)
        {
            string marker = i == _viewModel.SelectedActionIndex ? ">" : " ";
            _renderer.DrawText($"{marker} {i + 1}. {_viewModel.Actions[i].Label}", new Position(1240, 430 + (i * 30)));
        }

        _renderer.DrawText("Hints", new Position(1240, 650));
        for (int i = 0; i < _viewModel.Hints.Count; i++)
            _renderer.DrawText($"{i + 1}. {_viewModel.Hints[i].Description.Label}", new Position(1240, 690 + (i * 30)));
    }

    private static string FormatCards(IReadOnlyCollection<Core.Models.Card> cards, int maxCards)
    {
        if (cards.Count == 0)
            return "(empty)";

        string shown = string.Join(", ", cards.Take(maxCards).Select(card => $"{card.Rank} {card.Suit}"));
        return cards.Count > maxCards ? $"{shown}, +{cards.Count - maxCards}" : shown;
    }
}
