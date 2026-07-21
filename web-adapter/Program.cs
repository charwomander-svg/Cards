using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Cards.Xbox;
using System.Text.Json;

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
            // expose pile list as simple metadata array so client can map pile ids to display names if desired
            pileList = snap.Piles.Select(kv => new { id = kv.Key, name = kv.Key }).ToList()
        };
        return Results.Json(data);
});

        app.MapGet("/api/session/actions", () => Results.Json(viewModel.Actions.Select(a => new {
            label = a.Label,
            help = a.HelpText,
            cards = a.Move.Cards,
            expectedSelection = new {
                count = (a.Move.Cards?.Count ?? a.Move.Count),
                requiresCards = (a.Move.Cards != null && a.Move.Cards.Count > 0),
                source = a.Move.Source,
                destination = a.Move.Destination,
                // enrich with partial/ordered flags to help client matching
                allowPartial = false,
                ordered = false
            }
        })));

        // Given a selection of card titles, return matching actions (indices + labels) that accept that selection
        app.MapPost("/api/session/selection-preview", async (HttpContext ctx) => {
            var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,object>>();
            var selected = body?.GetValueOrDefault("selected") as JsonElement?;
            List<string> selList = new();
            if (selected.HasValue && selected.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var el in selected.Value.EnumerateArray())
                    if (el.ValueKind == System.Text.Json.JsonValueKind.String) selList.Add(el.GetString()!);
            }
            var matches = viewModel.Actions
                .Select((a, i) => new { Index = i, Label = a.Label, Cards = a.Move.Cards })
                .Where(x => {
                    if (x.Cards is null) return false;
                    var cset = new HashSet<string>(x.Cards, StringComparer.OrdinalIgnoreCase);
                    return cset.SetEquals(selList);
                })
                .Select(x => new { index = x.Index, label = x.Label })
                .ToList();
            return Results.Json(new { matches });
        });

        app.MapPost("/api/session/action", async (HttpContext ctx) => {
            var raw = await ctx.Request.ReadFromJsonAsync<Dictionary<string,object>>();
            if (raw is null) return Results.Json(new { message = "invalid body" });

            int index = -1;
            if (raw.TryGetValue("index", out var idxObj) && idxObj is JsonElement je && je.ValueKind == System.Text.Json.JsonValueKind.Number)
                index = je.GetInt32();

            List<string> selected = new();
            if (raw.TryGetValue("selected", out var selObj) && selObj is JsonElement selEl && selEl.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var el in selEl.EnumerateArray()) if (el.ValueKind == System.Text.Json.JsonValueKind.String) selected.Add(el.GetString()!);
            }

            // If an index was provided, prefer that action if it either requires no explicit cards or its Move.Cards matches the selection
            if (index >= 0 && index < viewModel.Actions.Count)
            {
                var candidate = viewModel.Actions[index];
                var moveCards = candidate.Move.Cards;
                if (moveCards is null || moveCards.Count == 0 || (selected.Count > 0 && new HashSet<string>(moveCards, StringComparer.OrdinalIgnoreCase).SetEquals(selected)))
                {
                    var result = viewModel.ApplySelectedAction(index);
                    return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
                }
            }

            // Otherwise try to find any action whose Move.Cards exactly match the selection
            var matches = viewModel.Actions
                .Select((a, i) => new { Index = i, Label = a.Label, Cards = a.Move.Cards })
                .Where(x => {
                    if (x.Cards is null) return false;
                    var cset = new HashSet<string>(x.Cards, StringComparer.OrdinalIgnoreCase);
                    return cset.SetEquals(selected);
                })
                .ToList();

            if (matches.Count == 1)
            {
                var chosen = matches[0];
                var result = viewModel.ApplySelectedAction(chosen.Index);
                var move = viewModel.Actions[chosen.Index].Move;
                                var appliedInfo = new {
                                    index = chosen.Index,
                                    label = chosen.Label,
                                    cards = move.Cards,
                                    // prefer explicit pile ids when Move.Destination looks like a pile key; include both for safety
                                    source = move.Source,
                                    sourcePileId = move.Source,
                                    destination = move.Destination,
                                    destinationPileId = move.Destination
                                };
                                return Results.Json(new { message = result.Message, applied = appliedInfo, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
            }

            if (matches.Count > 1)
            {
                return Results.Json(new { message = "ambiguous selection", choices = matches.Select(m => new { index = m.Index, label = m.Label }) });
            }

            // Fallback: if selection empty, try applying index-less default action
            if (selected.Count == 0 && index >= 0)
            {
                var result = viewModel.ApplySelectedAction(index);
                return Results.Json(new { message = result.Message, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
            }

            return Results.Json(new { message = "no matching action for selection" });
        });

app.Run();
