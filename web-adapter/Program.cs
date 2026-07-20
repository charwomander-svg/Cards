using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Cards.Xbox;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(opts => { opts.SerializerOptions.WriteIndented = true; });

var app = builder.Build();

// Serve static files from ../web-ui
var webUiPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "web-ui");
if (!Directory.Exists(webUiPath)) {
    Console.WriteLine($"web-ui folder not found at {webUiPath}. Create web-ui in the repo root.");
}
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".js"] = "application/javascript";

app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
app.UseStaticFiles(new StaticFileOptions { FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(webUiPath), ContentTypeProvider = provider });

// Integrate with the real engine via CollectionUiViewModel
var viewModel = new CollectionUiViewModel();

app.MapGet("/api/session/status", () => Results.Json(new { undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel }));

app.MapPost("/api/session/undo", async (HttpContext ctx) => {
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,int>>();
    var count = body?.GetValueOrDefault("count", 1) ?? 1;
    var result = viewModel.UndoLast(count);
    return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
});

app.MapPost("/api/session/redo", async (HttpContext ctx) => {
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,int>>();
    var count = body?.GetValueOrDefault("count", 1) ?? 1;
    var result = viewModel.RedoLast(count);
    return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
});

app.MapPost("/api/session/start", async (HttpContext ctx) => {
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,string>>();
    var gameName = body?.GetValueOrDefault("gameName") ?? "Klondike";
    // find matching game in catalog
    var index = viewModel.VisibleGames.ToList().FindIndex(g => g.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
    if (index >= 0) viewModel.MoveSelection(index - viewModel.SelectedGameIndex);
    var result = viewModel.StartSelectedGame();
    return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
});

app.MapPost("/api/session/save", () => {
    var r = viewModel.SaveSession();
    return Results.Json(new { message = r.Message, saved = viewModel.SavedSessionJson != null });
});
app.MapPost("/api/session/load", () => {
    var r = viewModel.LoadSavedSession();
    return Results.Json(new { message = r.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
});

// Richer endpoints for UI
app.MapGet("/api/games", () => Results.Json(viewModel.VisibleGames.Select(g => new { name = g.Name, players = g.Players, category = g.Category, objective = g.Objective })));

app.MapGet("/api/session/snapshot", () => {
    var snap = viewModel.Snapshot();
    if (snap is null) return Results.Json(new { message = "no session" });
    var data = new {
        gameName = snap.GameName,
        isComplete = snap.IsComplete,
        scores = snap.Scores,
        hands = snap.Hands.ToDictionary(kv => kv.Key, kv => kv.Value.Select(c => c.ToString()).ToList()),
                piles = snap.Piles.ToDictionary(kv => kv.Key, kv => kv.Value.Select(c => c.ToString()).ToList()),
    };
    return Results.Json(data);
});

        app.MapGet("/api/session/actions", () => Results.Json(viewModel.Actions.Select(a => new { label = a.Label, help = a.HelpText, cards = a.Move.Cards })));

        app.MapPost("/api/session/action", async (HttpContext ctx) => {
            var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,int>>();
            var index = body?.GetValueOrDefault("index", -1) ?? -1;
            if (index < 0) return Results.Json(new { message = "invalid index" });
            var result = viewModel.ApplySelectedAction(index);
            return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
        });

app.Run();
