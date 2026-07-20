using Cards.Core.Data;
using Cards.Core.Engine;
using Cards.Core.Engine.Actions;
using Cards.Core.Engine.Autoplay;
using Cards.Core.Engine.Replay;
using Cards.Core.Engine.Rules;
using Cards.Core.Engine.Hints;
using Cards.Core.Engine.Serialization;
using Cards.Core.Engine.Validation;

namespace Cards.Xbox;

public sealed class CollectionUiViewModel
{
    private static readonly string[] SearchPresets =
    {
        string.Empty,
        "Solitaire",
        "Rummy",
        "Bridge",
        "Euchre",
        "Pinochle",
        "Shedding",
        "Casino"
    };

    private readonly CardGameEngine _engine;
    private readonly HintProvider _hints;
    private readonly AutoplayRunner _autoplay;
    private readonly SessionReplay _replay;
    private readonly List<CardGameRuleSet> _visibleGames;
    private readonly HashSet<string> _favorites = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<string> _recentGames = new();
    private CardGameSession? _session;
    private IReadOnlyList<ActionDescription> _actions = Array.Empty<ActionDescription>();
    private IReadOnlyList<Hint> _currentHints = Array.Empty<Hint>();
    private readonly List<MoveDescriptor> _redoStack = new();
    private string _filter = string.Empty;
    private int _configuredPlayerCount = 1;
    private ValidationResult _validation = ValidationResult.Valid;

    public CollectionUiViewModel(CardGameEngine? engine = null)
    {
        _engine = engine ?? new CardGameEngine();
        _hints = new HintProvider(_engine);
        _autoplay = new AutoplayRunner(_engine);
        _replay = new SessionReplay(_engine);
        _visibleGames = CardGameCatalog.Games.OrderBy(game => game.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public IReadOnlyList<CardGameRuleSet> VisibleGames => _visibleGames;
    public IReadOnlyList<CardGameStyleGroup> Categories => CardGameCatalog.StyleGroups;
    public int SelectedCategoryIndex { get; private set; } = -1;
    public string ActiveCategoryName => SelectedCategoryIndex < 0 ? "All Games" : Categories[SelectedCategoryIndex].Name;
    public int SelectedGameIndex { get; private set; }
    public CardGameRuleSet SelectedGame => _visibleGames[SelectedGameIndex];
    public CardGameSession? Session => _session;
    public IReadOnlyList<ActionDescription> Actions => _actions;
    public IReadOnlyList<Hint> Hints => _currentHints;
    public int SelectedActionIndex { get; private set; }
    public int TutorialStepIndex { get; private set; }
    public int ConfiguredPlayerCount => _configuredPlayerCount;
    public CollectionUiMode Mode { get; private set; } = CollectionUiMode.Library;
    public string? SavedSessionJson { get; private set; }
    public IReadOnlyCollection<string> Favorites => _favorites;
    public IReadOnlyList<string> RecentGames => _recentGames;
    public int SearchPresetIndex { get; private set; }
    public string ActiveSearchPreset => SearchPresets[SearchPresetIndex].Length == 0 ? "All" : SearchPresets[SearchPresetIndex];
    public AutoplayResult? LastAutoplayResult { get; private set; }
    public ValidationResult Validation => _validation;
    public string Filter => _filter;
    public string Status { get; private set; } = "Browse the collection and press A to start.";

    // Undo/Redo availability counts
    public int UndoAvailableCount => _session?.History.Count ?? 0;
    public int RedoAvailableCount => _redoStack.Count;

    // Top redo action label (for UI previews)
    public string? TopRedoActionLabel
    {
        get
        {
            if (_redoStack.Count == 0) return null;
            var move = _redoStack[^1];
            var desc = ActionDescriptionProvider.Describe(new[] { move }).FirstOrDefault();
            return desc?.Label ?? move.Type + (string.IsNullOrWhiteSpace(move.Source) ? string.Empty : $" {move.Source}");
        }
    }

    public void MoveSelection(int delta)
    {
        if (_visibleGames.Count == 0)
            return;

        SelectedGameIndex = Math.Clamp(SelectedGameIndex + delta, 0, _visibleGames.Count - 1);
        TutorialStepIndex = 0;
        _configuredPlayerCount = ParsePlayerCount(SelectedGame.Players);
    }

    public void ApplyFilter(string filter)
    {
        _filter = filter ?? string.Empty;
        _visibleGames.Clear();
        IEnumerable<CardGameRuleSet> games = CardGameCatalog.Games;
        if (SelectedCategoryIndex >= 0)
        {
            HashSet<string> categoryGames = Categories[SelectedCategoryIndex].GameNames.ToHashSet(StringComparer.OrdinalIgnoreCase);
            games = games.Where(game => categoryGames.Contains(game.Name));
        }

        if (!string.IsNullOrWhiteSpace(_filter))
        {
            games = games.Where(game =>
                game.Name.Contains(_filter, StringComparison.OrdinalIgnoreCase)
                || game.Category.Contains(_filter, StringComparison.OrdinalIgnoreCase)
                || game.Objective.Contains(_filter, StringComparison.OrdinalIgnoreCase));
        }

        _visibleGames.AddRange(games
            .OrderByDescending(game => game.Name.Equals(_filter, StringComparison.OrdinalIgnoreCase))
            .ThenBy(game => game.Name, StringComparer.OrdinalIgnoreCase));
        SelectedGameIndex = _visibleGames.Count == 0 ? 0 : Math.Min(SelectedGameIndex, _visibleGames.Count - 1);
        TutorialStepIndex = 0;
        _configuredPlayerCount = _visibleGames.Count == 0 ? 1 : ParsePlayerCount(SelectedGame.Players);
    }

    public void MoveSearchPreset(int delta)
    {
        SearchPresetIndex = (SearchPresetIndex + delta) % SearchPresets.Length;
        if (SearchPresetIndex < 0)
            SearchPresetIndex += SearchPresets.Length;

        ApplyFilter(SearchPresets[SearchPresetIndex]);
        Status = $"Search preset: {ActiveSearchPreset}.";
    }

    public MoveResult StartSelectedGame(int? playerCount = null)
    {
        int players = playerCount ?? _configuredPlayerCount;
        try
        {
            _session = _engine.StartGame(SelectedGame.Name, players);
            RefreshActions();
            RefreshValidation();
            TrackRecent(SelectedGame.Name);
            Mode = CollectionUiMode.Table;
            Status = $"Started {SelectedGame.Name}.";
            return MoveResult.Success(Status);
        }
        catch (KeyNotFoundException)
        {
            _session = null;
            _actions = Array.Empty<ActionDescription>();
            _currentHints = Array.Empty<Hint>();
            _validation = ValidationResult.Valid;
            Mode = CollectionUiMode.Tutorial;
            Status = $"{SelectedGame.Name} has tutorial rules, but no playable engine yet.";
            return MoveResult.Failure(Status);
        }
    }

    public MoveResult ApplySelectedAction(int actionIndex = 0)
    {
        if (_session is null)
            return MoveResult.Failure("Start a game first.");
        int selectedIndex = actionIndex < 0 ? SelectedActionIndex : actionIndex;
        if (selectedIndex < 0 || selectedIndex >= _actions.Count)
            return MoveResult.Failure("No action is selected.");

        MoveResult result = _engine.ApplyMove(_session, _actions[selectedIndex].Move);
        // any new manual move clears the redo stack
        _redoStack.Clear();
        Status = result.Message;
        RefreshActions();
        RefreshValidation();
        return result;
    }

    public MoveResult RunAutoplayStep()
    {
        if (_session is null)
            return MoveResult.Failure("Start a game before autoplay.");

        MoveResult result = _autoplay.Step(_session);
        Status = result.Message;
        RefreshActions();
        RefreshValidation();
        LastAutoplayResult = new AutoplayResult(1, result.Succeeded ? 1 : 0, _session.IsComplete);
        return result;
    }

    public MoveResult UndoLast()
    {
            return UndoLast(1);
        }

        public MoveResult UndoLast(int count)
        {
            if (_session is null)
                return MoveResult.Failure("Start a game first.");
            try
            {
                if (_session.History.Count == 0)
                    return MoveResult.Failure("No moves to undo.");

                int toUndo = Math.Clamp(count, 1, _session.History.Count);
                // capture the last 'toUndo' moves for redo (preserve order so redo reapplies last undone first)
                var lastMoves = _session.History.Skip(_session.History.Count - toUndo).Select(h => h.Move).ToList();
                // push in order so last undone is on top
                foreach (var m in lastMoves)
                    _redoStack.Add(m);

                // replay up to remaining history count
                var replayResult = _replay.Replay(_session.GameName, _session.PlayerCount, _session.Seed, _session.History.Take(_session.History.Count - toUndo).Select(h => h.Move));
                _session = replayResult.Session;
                RefreshActions();
                RefreshValidation();
                Status = toUndo == 1 ? "Undid last move." : $"Undid {toUndo} moves.";
                return MoveResult.Success(Status);
            }
            catch (Exception ex)
            {
                return MoveResult.Failure($"Undo failed: {ex.Message}");
            }
        }

    public AutoplayResult RunAutoplay(int maxMoves = 5)
    {
        if (_session is null)
        {
            LastAutoplayResult = new AutoplayResult(0, 0, false);
            Status = "Start a game before autoplay.";
            return LastAutoplayResult;
        }

        LastAutoplayResult = _autoplay.Run(_session, maxMoves);
        Status = $"Autoplay: {LastAutoplayResult.MovesSucceeded}/{LastAutoplayResult.MovesAttempted} moves.";
        RefreshActions();
        RefreshValidation();
        return LastAutoplayResult;
    }

    public void MoveActionSelection(int delta)
    {
        if (_actions.Count == 0)
        {
            SelectedActionIndex = 0;
            return;
        }

        SelectedActionIndex = Math.Clamp(SelectedActionIndex + delta, 0, _actions.Count - 1);
    }

    public MoveResult SaveSession()
    {
        if (_session is null)
            return MoveResult.Failure("Start a game before saving.");

        SavedSessionJson = SessionSerializer.ToJson(_session);
        Status = $"Saved {_session.GameName}.";
        return MoveResult.Success(Status);
    }

    public MoveResult LoadSavedSession()
    {
        if (string.IsNullOrWhiteSpace(SavedSessionJson))
            return MoveResult.Failure("No saved game is available.");

        _session = SessionSerializer.RestoreJson(SavedSessionJson);
        RefreshActions();
        RefreshValidation();
        Mode = CollectionUiMode.Table;
        Status = $"Loaded {_session.GameName}.";
        return MoveResult.Success(Status);
    }

    public MoveResult RedoLast()
    {
            return RedoLast(1);
        }

        public MoveResult RedoLast(int count)
        {
            if (_session is null)
                return MoveResult.Failure("Start a game first.");
            if (_redoStack.Count == 0)
                return MoveResult.Failure("No moves to redo.");

            try
            {
                int toRedo = Math.Clamp(count, 1, _redoStack.Count);
                MoveResult lastResult = MoveResult.Failure("No redo performed.");
                // reapply in LIFO order (most recently undone first)
                for (int i = 0; i < toRedo; i++)
                {
                    var move = _redoStack[^1];
                    _redoStack.RemoveAt(_redoStack.Count - 1);
                    lastResult = _engine.ApplyMove(_session, move);
                    if (!lastResult.Succeeded)
                    {
                        // stop on failure
                        Status = lastResult.Message;
                        RefreshActions();
                        RefreshValidation();
                        return lastResult;
                    }
                }

                Status = toRedo == 1 ? "Redid last move." : $"Redid {toRedo} moves.";
                RefreshActions();
                RefreshValidation();
                return MoveResult.Success(Status);
            }
            catch (Exception ex)
            {
                return MoveResult.Failure($"Redo failed: {ex.Message}");
            }
        }

    public GameSnapshot? Snapshot() => _session?.CreateSnapshot();

    public SessionUiSummary? BuildSessionSummary()
    {
        GameSnapshot? snapshot = Snapshot();
        if (snapshot is null)
            return null;

        var leader = snapshot.Scores
            .OrderByDescending(pair => pair.Value)
            .ThenBy(pair => pair.Key)
            .FirstOrDefault();

        return new SessionUiSummary(
            snapshot.GameName,
            _actions.Count,
            snapshot.Hands.Count,
            snapshot.Piles.Count,
            snapshot.Scores.Count == 0 ? null : leader.Key,
            snapshot.Scores.Count == 0 ? 0 : leader.Value,
            snapshot.IsComplete);
    }

    public void SetMode(CollectionUiMode mode)
    {
        if (mode == CollectionUiMode.Table && _session is null)
            Mode = CollectionUiMode.Library;
        else
            Mode = mode;
    }

    public void AdjustPlayerCount(int delta)
    {
        _configuredPlayerCount = Math.Clamp(_configuredPlayerCount + delta, 1, 8);
        Status = $"Players set to {_configuredPlayerCount}.";
    }

    public void MoveTutorialStep(int delta)
    {
        int maxIndex = Math.Max(0, SelectedGame.HowToPlay.Count - 1);
        TutorialStepIndex = Math.Clamp(TutorialStepIndex + delta, 0, maxIndex);
    }

    public void MoveCategory(int delta)
    {
        int categoryCount = Categories.Count;
        SelectedCategoryIndex += delta;
        if (SelectedCategoryIndex < -1)
            SelectedCategoryIndex = categoryCount - 1;
        if (SelectedCategoryIndex >= categoryCount)
            SelectedCategoryIndex = -1;

        ApplyFilter(_filter);
        Status = $"Category: {ActiveCategoryName}.";
    }

    public void ToggleFavorite()
    {
        string gameName = SelectedGame.Name;
        if (!_favorites.Add(gameName))
        {
            _favorites.Remove(gameName);
            Status = $"Removed favorite: {gameName}.";
        }
        else
        {
            Status = $"Added favorite: {gameName}.";
        }
    }

    public bool IsFavorite(string gameName) => _favorites.Contains(gameName);

    private void RefreshActions()
    {
        if (_session is null)
        {
            _actions = Array.Empty<ActionDescription>();
            _currentHints = Array.Empty<Hint>();
            return;
        }

        _actions = ActionDescriptionProvider.Describe(_engine.GetLegalMoves(_session));
        _currentHints = _hints.GetHints(_session, Math.Min(3, Math.Max(1, _actions.Count)));
        SelectedActionIndex = Math.Min(SelectedActionIndex, Math.Max(0, _actions.Count - 1));
    }

    private void RefreshValidation()
    {
        _validation = _session is null ? ValidationResult.Valid : _engine.ValidateSession(_session);
    }

    private static int ParsePlayerCount(string players)
    {
        string firstNumber = new(players.TakeWhile(char.IsDigit).ToArray());
        return int.TryParse(firstNumber, out int parsed) ? Math.Max(1, parsed) : 1;
    }

    private void TrackRecent(string gameName)
    {
        _recentGames.RemoveAll(name => name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
        _recentGames.Insert(0, gameName);
        if (_recentGames.Count > 5)
            _recentGames.RemoveRange(5, _recentGames.Count - 5);
    }
}
