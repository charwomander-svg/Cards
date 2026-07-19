using System.Reflection;
using System.Text.Json;

namespace Cards.Core.Tutorials;

/// <summary>Built-in tutorials and source-backed rules summaries for the card game collection.</summary>
public static class TutorialLibrary
{
    private const string CatalogResourceName = "CardGameTutorials.json";

    public static IReadOnlyList<GameTutorial> All { get; } = LoadTutorials().AsReadOnly();

    public static GameTutorial GetByName(string gameName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(gameName);
        return All.FirstOrDefault(tutorial => string.Equals(tutorial.GameName, gameName, StringComparison.OrdinalIgnoreCase))
            ?? throw new ArgumentException($"No tutorial found for game: {gameName}", nameof(gameName));
    }

    private static List<GameTutorial> LoadTutorials()
    {
        using Stream stream = OpenCatalogResource();
        var entries = JsonSerializer.Deserialize<List<TutorialCatalogEntry>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Tutorial catalog could not be loaded.");

        return entries
            .Select(entry => new GameTutorial(
                entry.GameName,
                entry.Objective,
                new[]
                {
                    new TutorialStep("Overview", entry.Objective),
                    new TutorialStep("Category", entry.Category),
                    new TutorialStep("Sources", entry.Sources)
                },
                entry.Category,
                entry.Sources))
            .ToList();
    }

    private static Stream OpenCatalogResource()
    {
        Assembly assembly = typeof(TutorialLibrary).Assembly;
        string? resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(name => name.EndsWith(CatalogResourceName, StringComparison.Ordinal));

        return resourceName is null
            ? throw new InvalidOperationException($"Embedded tutorial catalog resource '{CatalogResourceName}' was not found.")
            : assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Embedded tutorial catalog resource '{CatalogResourceName}' could not be opened.");
    }

    private sealed class TutorialCatalogEntry
    {
        public string GameName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Sources { get; set; } = string.Empty;
    }
}
