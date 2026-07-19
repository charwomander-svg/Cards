namespace Cards.Core.Tutorials;

/// <summary>Built-in tutorials for the card game collection.</summary>
public static class TutorialLibrary
{
    public static IReadOnlyList<GameTutorial> All { get; } = new List<GameTutorial>
    {
        new(
            "War",
            "Win the entire deck by taking higher-card battles.",
            new[]
            {
                new TutorialStep("Setup", "Shuffle the deck and deal every card evenly face down between two players."),
                new TutorialStep("Turn", "Each player flips their top card at the same time. The higher rank wins both cards."),
                new TutorialStep("War", "If ranks tie, each tied player places three cards face down and one card face up. Highest face-up card wins the pile."),
                new TutorialStep("Win", "A player wins after collecting all cards, or by holding more cards when play ends.")
            }),
        new(
            "Blackjack",
            "Build a hand closer to 21 than the dealer without going over.",
            new[]
            {
                new TutorialStep("Card Values", "Number cards use face value, face cards count as 10, and aces count as 1 or 11."),
                new TutorialStep("Player Turn", "Choose hit to draw another card or stand to keep the current total."),
                new TutorialStep("Dealer Turn", "The dealer reveals their hidden card and draws until reaching at least 17."),
                new TutorialStep("Win", "Beat the dealer's total without busting. Blackjack is an ace plus a ten-value card.")
            }),
        new(
            "Five-Card Draw",
            "Make the strongest five-card poker hand after one draw.",
            new[]
            {
                new TutorialStep("Setup", "Deal five private cards to each player, then run an opening betting round."),
                new TutorialStep("Draw", "Players may discard any number of cards and draw replacements from the deck."),
                new TutorialStep("Showdown", "After final betting, remaining players reveal hands and compare standard poker ranks."),
                new TutorialStep("Win", "The best hand wins the pot, from high card up through royal flush.")
            }),
        new(
            "Go Fish",
            "Collect the most four-card books of matching ranks.",
            new[]
            {
                new TutorialStep("Setup", "Deal five cards to each player, or seven cards when only two people play."),
                new TutorialStep("Ask", "On your turn, ask one player for a rank you already hold."),
                new TutorialStep("Go Fish", "If they have none, draw from the deck. If the draw matches the asked rank, continue your turn."),
                new TutorialStep("Win", "Lay down books of four matching ranks. Most books wins after all books are made.")
            }),
        new(
            "Crazy Eights",
            "Be the first player to empty your hand by matching rank or suit.",
            new[]
            {
                new TutorialStep("Setup", "Deal five cards to each player and turn one card face up to start the discard pile."),
                new TutorialStep("Play", "Play a card matching the discard pile's rank or suit. Draw if you cannot play."),
                new TutorialStep("Eights", "An eight may be played on any card, and the player names the next suit."),
                new TutorialStep("Win", "First empty hand wins. Remaining cards can be scored against the other players.")
            }),
        new(
            "Solitaire",
            "Build all four foundations from ace to king by suit.",
            new[]
            {
                new TutorialStep("Setup", "Deal seven tableau columns, increasing from one to seven cards with only the top card face up."),
                new TutorialStep("Tableau", "Build descending sequences in alternating colors and move face-up runs between columns."),
                new TutorialStep("Stock", "Draw from the stock when no tableau move is available."),
                new TutorialStep("Win", "Move every card to foundations in ascending suit order from ace through king.")
            })
    }.AsReadOnly();

    public static GameTutorial GetByName(string gameName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(gameName);
        return All.First(tutorial => string.Equals(tutorial.GameName, gameName, StringComparison.OrdinalIgnoreCase));
    }
}
