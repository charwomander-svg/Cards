namespace Cards.Core.Tutorials;

/// <summary>Compiled rules and guided tutorial pages for a single card game.</summary>
public sealed class GameTutorial
{
    public GameTutorial(string gameName, string objective, IEnumerable<TutorialStep> steps)
    {
        GameName = string.IsNullOrWhiteSpace(gameName)
            ? throw new ArgumentException("A tutorial needs a game name.", nameof(gameName))
            : gameName;
        Objective = string.IsNullOrWhiteSpace(objective)
            ? throw new ArgumentException("A tutorial needs an objective.", nameof(objective))
            : objective;

        var stepList = steps?.ToList() ?? throw new ArgumentNullException(nameof(steps));
        if (stepList.Count == 0)
            throw new ArgumentException("A tutorial needs at least one step.", nameof(steps));

        Steps = stepList.AsReadOnly();
    }

    public string GameName { get; }
    public string Objective { get; }
    public IReadOnlyList<TutorialStep> Steps { get; }
}
