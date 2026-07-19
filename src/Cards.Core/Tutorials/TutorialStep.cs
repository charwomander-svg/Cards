namespace Cards.Core.Tutorials;

/// <summary>One page of tutorial content for a card game.</summary>
public sealed class TutorialStep
{
    public TutorialStep(string title, string body)
    {
        Title = string.IsNullOrWhiteSpace(title)
            ? throw new ArgumentException("A tutorial step needs a title.", nameof(title))
            : title;
        Body = string.IsNullOrWhiteSpace(body)
            ? throw new ArgumentException("A tutorial step needs body text.", nameof(body))
            : body;
    }

    public string Title { get; }
    public string Body { get; }
}
