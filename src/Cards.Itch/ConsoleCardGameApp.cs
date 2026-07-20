using Cards.Xbox;

namespace Cards.Itch;

public sealed class ConsoleCardGameApp
{
    private readonly TextReader _input;
    private readonly TextWriter _output;
    private readonly CollectionUiViewModel _viewModel;

    public ConsoleCardGameApp(TextReader input, TextWriter output, CollectionUiViewModel? viewModel = null)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _output = output ?? throw new ArgumentNullException(nameof(output));
        _viewModel = viewModel ?? new CollectionUiViewModel();
    }

    public CollectionUiViewModel ViewModel => _viewModel;

    public void Run()
    {
        PrintWelcome();
        PrintHelp();
        PrintLibrary();

        while (true)
        {
            _output.Write("> ");
            string? command = _input.ReadLine();
            if (command is null || Execute(command))
                break;
        }
    }

    public bool Execute(string commandLine)
    {
        string[] parts = commandLine.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return false;

        string command = parts[0].ToLowerInvariant();
        string argument = parts.Length > 1 ? parts[1] : string.Empty;

        switch (command)
        {
            case "help":
                PrintHelp();
                break;
            case "list":
                PrintLibrary();
                break;
            case "search":
                _viewModel.ApplyFilter(argument);
                PrintLibrary();
                break;
            case "category":
                MoveCategory(argument);
                PrintLibrary();
                break;
            case "select":
                SelectGame(argument);
                PrintSelectedGame();
                break;
            case "rules":
                PrintSelectedGameFull();
                break;
            case "players":
                SetPlayers(argument);
                break;
            case "start":
                if (!string.IsNullOrWhiteSpace(argument) && int.TryParse(argument, out int startIndex))
                {
                    // allow "start <number>" to select then start that visible game
                    SelectGame(argument);
                }
                try
                {
                    var startResult = _viewModel.StartSelectedGame();
                    if (startResult.Message != null && startResult.Message.Contains("tutorial rules", StringComparison.OrdinalIgnoreCase))
                    {
                        _output.WriteLine("Selected game has tutorial-only rules (no playable engine). Use 'list' or 'search' to pick another or implement the rule set.");
                    }
                    else
                    {
                        PrintResult(startResult);
                        PrintTable();
                    }
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"Error starting game: {ex.Message}");
                }
                break;
            case "actions":
                PrintActions();
                break;
            case "undo":
                            int undoCount = 1;
                            if (!string.IsNullOrWhiteSpace(argument) && (!int.TryParse(argument, out undoCount) || undoCount < 1))
                            {
                                _output.WriteLine("Enter a positive number of moves to undo.");
                                break;
                            }
                            PrintResult(_viewModel.UndoLast(undoCount));
                            PrintTable();
                            break;
                        case "redo":
                            int redoCount = 1;
                            if (!string.IsNullOrWhiteSpace(argument) && (!int.TryParse(argument, out redoCount) || redoCount < 1))
                            {
                                _output.WriteLine("Enter a positive number of moves to redo.");
                                break;
                            }
                            PrintResult(_viewModel.RedoLast(redoCount));
                            PrintTable();
                            break;
            case "play":
                PlayAction(argument);
                PrintTable();
                break;
            case "auto":
                RunAutoplay(argument);
                PrintTable();
                break;
            case "hint":
            case "hints":
                PrintHints();
                break;
            case "save":
                PrintResult(_viewModel.SaveSession());
                break;
            case "load":
                PrintResult(_viewModel.LoadSavedSession());
                PrintTable();
                break;
            case "status":
                PrintTable();
                break;
            case "ascii":
                PrintAsciiPiles(argument);
                break;
            case "quit":
            case "exit":
                _output.WriteLine("Thanks for playing.");
                return true;
            default:
                _output.WriteLine("Unknown command. Type 'help' for commands.");
                break;
        }

        return false;
    }

    private void PrintWelcome()
    {
        _output.WriteLine("The Ultimate Card Game Collection - Itch.io Demo");
        _output.WriteLine("Browse 500+ rule sets, play supported engines, and use hints/autoplay.");
        _output.WriteLine();
    }

    private void PrintHelp()
    {
            _output.WriteLine("Commands: list, search <text>, category next|prev, select <number>, rules, players <n>, start, actions, play <number>, hints, auto [n], save, load, status, ascii, quit");
    }

    private void PrintLibrary()
    {
        _output.WriteLine();
        _output.WriteLine($"Library - {_viewModel.VisibleGames.Count} games | Category: {_viewModel.ActiveCategoryName} | Search: {_viewModel.Filter}");
        int count = Math.Min(20, _viewModel.VisibleGames.Count);
        for (int i = 0; i < count; i++)
        {
            var game = _viewModel.VisibleGames[i];
            string marker = i == _viewModel.SelectedGameIndex ? ">" : " ";
            _output.WriteLine($"{marker} {i + 1}. {game.Name} ({game.Category}, {game.Players})");
        }

        if (_viewModel.VisibleGames.Count > count)
            _output.WriteLine($"...and {_viewModel.VisibleGames.Count - count} more. Use search/category to narrow the list.");
    }

    private void PrintSelectedGame()
    {
        var game = _viewModel.SelectedGame;
        _output.WriteLine();
        _output.WriteLine($"{game.Name} - {game.Category} - {game.Players} player(s)");
        _output.WriteLine(game.Objective);
        // Do not auto-print the full HowToPlay/tutorial in common flows; use 'rules' to view them explicitly.
    }

    private void PrintSelectedGameFull()
    {
        var game = _viewModel.SelectedGame;
        _output.WriteLine();
        // Full rules shown on explicit 'rules' command.
        _output.WriteLine($"{game.Name} - {game.Category} - {game.Players} player(s)");
        _output.WriteLine(game.Objective);
        for (int i = 0; i < game.HowToPlay.Count; i++)
            _output.WriteLine($"{i + 1}. {game.HowToPlay[i]}");
    }

    private void PrintTable()
    {
        _output.WriteLine();
        _output.WriteLine(_viewModel.Status);
        SessionUiSummary? summary = _viewModel.BuildSessionSummary();
        if (summary is null)
            return;

        _output.WriteLine($"{summary.GameName}: {summary.LegalMoveCount} legal moves, {summary.HandCount} hands, {summary.PileCount} piles, valid: {_viewModel.Validation.IsValid}");
        // Undo/Redo UI indicators
        _output.WriteLine($"Undo available: {_viewModel.UndoAvailableCount}, Redo available: {_viewModel.RedoAvailableCount}");
        if (_viewModel.RedoAvailableCount > 0 && !string.IsNullOrWhiteSpace(_viewModel.TopRedoActionLabel))
            _output.WriteLine($"Next redo: {_viewModel.TopRedoActionLabel}");
        PrintActions();
    }

    private void PrintActions()
    {
        if (_viewModel.Actions.Count == 0)
        {
            _output.WriteLine("No legal actions.");
            return;
        }

        for (int i = 0; i < Math.Min(10, _viewModel.Actions.Count); i++)
            _output.WriteLine($"{i + 1}. {_viewModel.Actions[i].Label} - {_viewModel.Actions[i].HelpText}");
    }

    private void PrintHints()
    {
        if (_viewModel.Hints.Count == 0)
        {
            _output.WriteLine("No hints available.");
            return;
        }

        for (int i = 0; i < _viewModel.Hints.Count; i++)
            _output.WriteLine($"{i + 1}. {_viewModel.Hints[i].Description.Label}");
    }

    private void MoveCategory(string argument)
    {
        int delta = argument.Equals("prev", StringComparison.OrdinalIgnoreCase) ? -1 : 1;
        _viewModel.MoveCategory(delta);
    }

    private void SelectGame(string argument)
    {
        if (!int.TryParse(argument, out int oneBasedIndex) || oneBasedIndex < 1 || oneBasedIndex > _viewModel.VisibleGames.Count)
        {
            _output.WriteLine("Select a visible game number.");
            return;
        }

        _viewModel.MoveSelection((oneBasedIndex - 1) - _viewModel.SelectedGameIndex);
    }

    private void SetPlayers(string argument)
    {
        if (!int.TryParse(argument, out int desired) || desired < 1)
        {
            _output.WriteLine("Enter a player count from 1 to 8.");
            return;
        }

        _viewModel.AdjustPlayerCount(desired - _viewModel.ConfiguredPlayerCount);
        _output.WriteLine(_viewModel.Status);
    }

    private void PlayAction(string argument)
    {
        int index = 1;
        if (!string.IsNullOrWhiteSpace(argument) && (!int.TryParse(argument, out index) || index < 1))
        {
            _output.WriteLine("Enter an action number.");
            return;
        }

        PrintResult(_viewModel.ApplySelectedAction(index - 1));
    }

    private void RunAutoplay(string argument)
    {
        int moves = 5;
        if (!string.IsNullOrWhiteSpace(argument) && (!int.TryParse(argument, out moves) || moves < 1))
        {
            _output.WriteLine("Enter a positive autoplay move count.");
            return;
        }

        var result = _viewModel.RunAutoplay(moves);
        _output.WriteLine($"Autoplay completed {result.MovesSucceeded}/{result.MovesAttempted} moves.");
    }

    private void PrintResult(Core.Engine.MoveResult result)
    {
        _output.WriteLine(result.Message);
    }

    private void PrintAsciiPiles(string argument)
    {
        _output.WriteLine();
        var snapshot = _viewModel.Snapshot();
        if (snapshot is null)
        {
            _output.WriteLine("No active session. Start a game first.");
            return;
        }

        // Order piles by numeric suffix if available
        var ordered = snapshot.Piles
            .Select(pair => new { Name = pair.Key, Index = ParsePileIndex(pair.Key), Cards = pair.Value })
            .OrderBy(p => p.Index)
            .ThenBy(p => p.Name)
            .ToList();

        bool full = false;
        int columns = 8;
        if (!string.IsNullOrWhiteSpace(argument))
        {
            if (argument.Equals("full", StringComparison.OrdinalIgnoreCase))
                full = true;
            else if (int.TryParse(argument, out int parsed) && parsed > 0)
                columns = parsed;
        }

        int total = ordered.Count;
        if (full)
        {
            // Print each pile with its full stack vertically (limited height for readability)
            int maxHeight = ordered.Max(p => p.Cards?.Count ?? 0);
            maxHeight = Math.Min(maxHeight, 12); // cap height
            for (int h = maxHeight - 1; h >= 0; h--)
            {
                var parts = new List<string>();
                for (int i = 0; i < total; i++)
                {
                    var cards = ordered[i].Cards;
                    string txt = (cards is null || cards.Count <= h) ? ".." : CardToShort(cards[h]);
                    parts.Add(txt.PadRight(4));
                }

                _output.WriteLine(string.Join(' ', parts));
            }

            // Legend
            _output.WriteLine();
            for (int i = 0; i < Math.Min(20, ordered.Count); i++)
                _output.WriteLine($"{i + 1}. {ordered[i].Name}");

            return;
        }

        // Compact grid: show only top card for each pile in a fixed column width
        int rows = (int)Math.Ceiling(total / (double)columns);
        for (int r = 0; r < rows; r++)
        {
            var lineParts = new List<string>();
            for (int c = 0; c < columns; c++)
            {
                int idx = r + c * rows;
                if (idx >= total)
                {
                    lineParts.Add(string.Empty.PadRight(6));
                    continue;
                }

                var pile = ordered[idx];
                string top = (pile.Cards is null || pile.Cards.Count == 0) ? ".." : CardToShort(pile.Cards.Last());
                string label = top.PadRight(4);
                lineParts.Add(label);
            }

            _output.WriteLine(string.Join(' ', lineParts));
        }

        // Also print a small legend with pile names beneath for the first row
        _output.WriteLine();
        for (int c = 0; c < Math.Min(columns, ordered.Count); c++)
        {
            int idx = c * rows;
            var pile = ordered[idx];
            _output.WriteLine($"Col {c + 1}: {pile.Name}");
        }
    }

    private static int ParsePileIndex(string pileName)
    {
        int dash = pileName.LastIndexOf('-');
        if (dash < 0) return int.MaxValue;
        if (int.TryParse(pileName.Substring(dash + 1), out int idx)) return idx;
        return int.MaxValue;
    }

    private static string CardToShort(Cards.Core.Models.Card card)
    {
        // Example: Ace of Spades -> A♠, Ten -> 10
        string rank = card.Rank switch
        {
            Cards.Core.Models.Rank.Ace => "A",
            Cards.Core.Models.Rank.Jack => "J",
            Cards.Core.Models.Rank.Queen => "Q",
            Cards.Core.Models.Rank.King => "K",
            _ => ((int)card.Rank).ToString()
        };

        string suit = card.Suit switch
        {
            Cards.Core.Models.Suit.Clubs => "♣",
            Cards.Core.Models.Suit.Diamonds => "♦",
            Cards.Core.Models.Suit.Hearts => "♥",
            Cards.Core.Models.Suit.Spades => "♠",
            _ => "?"
        };

        return rank + suit;
    }
}

