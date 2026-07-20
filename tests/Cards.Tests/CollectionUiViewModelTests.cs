using Cards.Engine;
using Cards.Xbox;
using Xunit;

namespace Cards.Tests;

public class CollectionUiViewModelTests
{
    [Fact]
    public void ApplyFilter_NarrowsVisibleGames()
    {
        var viewModel = new CollectionUiViewModel();

        viewModel.ApplyFilter("Klondike");

        Assert.Contains(viewModel.VisibleGames, game => game.Name == "Klondike");
        Assert.All(viewModel.VisibleGames, game =>
            Assert.True(
                game.Name.Contains("Klondike", StringComparison.OrdinalIgnoreCase)
                || game.Category.Contains("Klondike", StringComparison.OrdinalIgnoreCase)
                || game.Objective.Contains("Klondike", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void StartSelectedGame_CreatesSessionActionsAndHints()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");

        var result = viewModel.StartSelectedGame();

        Assert.True(result.Succeeded);
        Assert.NotNull(viewModel.Session);
        Assert.NotEmpty(viewModel.Actions);
        Assert.NotEmpty(viewModel.Hints);
        Assert.NotNull(viewModel.Snapshot());
    }

    [Fact]
    public void ApplySelectedAction_UpdatesStatus()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        var result = viewModel.ApplySelectedAction();

        Assert.True(result.Succeeded);
        Assert.Equal(result.Message, viewModel.Status);
    }

    [Fact]
    public void MoveActionSelection_ChangesSelectedAction()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        viewModel.MoveActionSelection(1);

        int expected = viewModel.Actions.Count > 1 ? 1 : 0;
        Assert.Equal(expected, viewModel.SelectedActionIndex);
    }

    [Fact]
    public void SaveAndLoadSession_RoundTripsCurrentGame()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        var save = viewModel.SaveSession();
        var load = viewModel.LoadSavedSession();

        Assert.True(save.Succeeded);
        Assert.True(load.Succeeded);
        Assert.NotNull(viewModel.SavedSessionJson);
        Assert.Equal("Klondike", viewModel.Session?.GameName);
        Assert.NotEmpty(viewModel.Actions);
    }

    [Fact]
    public void AdjustPlayerCount_ChangesConfiguredPlayers()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Euchre");

        viewModel.AdjustPlayerCount(1);

        Assert.Equal(3, viewModel.ConfiguredPlayerCount);
        Assert.Contains("Players set to 3", viewModel.Status);
    }

    [Fact]
    public void ModeSwitching_PreventsTableWithoutSession()
    {
        var viewModel = new CollectionUiViewModel();

        viewModel.SetMode(CollectionUiMode.Table);
        Assert.Equal(CollectionUiMode.Library, viewModel.Mode);

        viewModel.SetMode(CollectionUiMode.Tutorial);
        Assert.Equal(CollectionUiMode.Tutorial, viewModel.Mode);
    }

    [Fact]
    public void MoveTutorialStep_TracksSelectedHowToStep()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");

        viewModel.MoveTutorialStep(1);

        Assert.Equal(1, viewModel.TutorialStepIndex);
    }

    [Fact]
    public void MoveCategory_FiltersVisibleGamesByStyleGroup()
    {
        var viewModel = new CollectionUiViewModel();

        viewModel.MoveCategory(1);

        Assert.Equal("Builder Solitaire", viewModel.ActiveCategoryName);
        Assert.All(viewModel.VisibleGames, game =>
            Assert.Contains(game.Name, viewModel.Categories[0].GameNames));
    }

    [Fact]
    public void ToggleFavorite_AddsAndRemovesSelectedGame()
    {
        var viewModel = new CollectionUiViewModel();
        string selected = viewModel.SelectedGame.Name;

        viewModel.ToggleFavorite();
        Assert.Contains(selected, viewModel.Favorites);

        viewModel.ToggleFavorite();
        Assert.DoesNotContain(selected, viewModel.Favorites);
    }

    [Fact]
    public void StartSelectedGame_TracksRecentGames()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");

        viewModel.StartSelectedGame();

        Assert.Equal("Klondike", viewModel.RecentGames[0]);
    }

    [Fact]
    public void MoveSearchPreset_AppliesPresetFilter()
    {
        var viewModel = new CollectionUiViewModel();

        viewModel.MoveSearchPreset(1);

        Assert.Equal("Solitaire", viewModel.ActiveSearchPreset);
        Assert.NotEmpty(viewModel.VisibleGames);
        Assert.All(viewModel.VisibleGames, game =>
            Assert.True(
                game.Name.Contains("Solitaire", StringComparison.OrdinalIgnoreCase)
                || game.Category.Contains("Solitaire", StringComparison.OrdinalIgnoreCase)
                || game.Objective.Contains("Solitaire", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void BuildSessionSummary_ReportsCurrentPlayableState()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        SessionUiSummary? summary = viewModel.BuildSessionSummary();

        Assert.NotNull(summary);
        Assert.Equal("Klondike", summary.GameName);
        Assert.True(summary.LegalMoveCount > 0);
        Assert.True(summary.PileCount > 0);
        Assert.False(summary.IsComplete);
    }

    [Fact]
    public void RunAutoplayStep_UpdatesAutoplayAndValidationState()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        var result = viewModel.RunAutoplayStep();

        Assert.True(result.Succeeded);
        Assert.NotNull(viewModel.LastAutoplayResult);
        Assert.True(viewModel.LastAutoplayResult.MovesAttempted >= 1);
        Assert.True(viewModel.Validation.IsValid);
    }

    [Fact]
    public void RunAutoplay_RunsMultipleSteps()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Klondike");
        viewModel.StartSelectedGame();

        var result = viewModel.RunAutoplay(3);

        Assert.True(result.MovesAttempted >= 1);
        Assert.Contains("Autoplay:", viewModel.Status);
    }

    [Fact]
    public void UnsupportedCatalogGame_StartsWithFallbackRule()
    {
        var viewModel = new CollectionUiViewModel();
        viewModel.ApplyFilter("Labyrinth");

        var result = viewModel.StartSelectedGame();

        // Fallback heuristics now map unknown catalog games to reasonable rule implementations.
        Assert.True(result.Succeeded);
        Assert.NotNull(viewModel.Session);
    }

    [Fact]
    public void CardCollectionGame_Update_RendersLibraryFrame()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        game.Update();

        IReadOnlyList<string> drawLog = renderer.GetFrameDrawLog();
        Assert.Contains(drawLog, entry => entry.Contains("The Ultimate Card Game Collection"));
        Assert.Contains(drawLog, entry => entry.Contains("Library:"));
        Assert.Contains(drawLog, entry => entry.Contains("Mode: Library"));
    }

    [Fact]
    public void CardCollectionGame_StartAndActionNavigation_RendersSelectedActionAndCards()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        game.ApplyFilter("Klondike");
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        Assert.True(input.IsJustPressed(XboxButton.A));
        game.Update();
        input.SetButtonState(XboxButton.A, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.DPadRight, ButtonState.Pressed);
        game.Update();

        IReadOnlyList<string> drawLog = renderer.GetFrameDrawLog();
        Assert.Contains(drawLog, entry => entry.Contains("Actions"));
        Assert.Contains(drawLog, entry => entry.Contains(">"));
        Assert.Contains(drawLog, entry => entry.Contains("hand:"));
        Assert.Contains(drawLog, entry => entry.Contains("Mode: Table"));
    }

    [Fact]
    public void CardCollectionGame_StartAndBack_LoadsSavedSession()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        game.ApplyFilter("Klondike");
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        Assert.True(input.IsJustPressed(XboxButton.A));
        game.Update();
        input.SetButtonState(XboxButton.A, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.Start, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.Start, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.Back, ButtonState.Pressed);
        game.Update();

        Assert.NotNull(game.ViewModel.Session);
        Assert.Contains(renderer.GetFrameDrawLog(), entry => entry.Contains("Loaded"));
    }

    [Fact]
    public void CardCollectionGame_LibraryPlayerCountAndTutorialControls_RenderModeState()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        input.SetButtonState(XboxButton.DPadRight, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.DPadRight, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.Y, ButtonState.Pressed);
        game.Update();

        Assert.Equal(CollectionUiMode.Tutorial, game.ViewModel.Mode);
        Assert.Equal(2, game.ViewModel.ConfiguredPlayerCount);
        Assert.Contains(renderer.GetFrameDrawLog(), entry => entry.Contains("Mode: Tutorial"));
    }

    [Fact]
    public void CardCollectionGame_CategoryFavoriteRecent_RenderInLibrary()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        input.SetButtonState(XboxButton.RightShoulder, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.RightShoulder, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.Back, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.Back, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.B, ButtonState.Pressed);
        game.Update();

        IReadOnlyList<string> drawLog = renderer.GetFrameDrawLog();
        Assert.Contains(drawLog, entry => entry.Contains("Category: Builder Solitaire"));
        Assert.Contains(drawLog, entry => entry.Contains(">*"));
        Assert.Contains(drawLog, entry => entry.Contains("Recent:"));
    }

    [Fact]
    public void CardCollectionGame_TableRendersPracticalSummary()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        game.ApplyFilter("Klondike");
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        game.Update();

        IReadOnlyList<string> drawLog = renderer.GetFrameDrawLog();
        Assert.Contains(drawLog, entry => entry.Contains("Search:"));
        Assert.Contains(drawLog, entry => entry.Contains("Summary:"));
    }

    [Fact]
    public void CardCollectionGame_TableRendersValidationAndAutoplay()
    {
        var input = new InputManager();
        var renderer = new RenderManager();
        var game = new CardCollectionGame(1, input, renderer);

        game.Initialize();
        game.ApplyFilter("Klondike");
        input.SetButtonState(XboxButton.A, ButtonState.Pressed);
        game.Update();
        input.SetButtonState(XboxButton.A, ButtonState.Released);
        game.Update();
        input.SetButtonState(XboxButton.RightShoulder, ButtonState.Pressed);
        game.Update();

        IReadOnlyList<string> drawLog = renderer.GetFrameDrawLog();
        Assert.Contains(drawLog, entry => entry.Contains("Validation:"));
        Assert.Contains(drawLog, entry => entry.Contains("Autoplay:"));
    }
}
