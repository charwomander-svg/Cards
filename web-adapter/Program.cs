using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Cards.Xbox;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

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
// in-memory store for client-provided pile layouts: pileId -> { left, top, width, height }
var pileLayouts = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
// optional store for last posted source items (list of { title, left, top, width, height })
var sourceItemsStore = new List<object>();
// recent applied info log for debugging
var appliedInfoLog = new List<object>();

static string NormalizeGameKey(string value)
{
    if (string.IsNullOrWhiteSpace(value)) return string.Empty;
    var formD = value.Normalize(NormalizationForm.FormD);
    var chars = formD
        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
        .Select(c => char.IsLetterOrDigit(c) ? char.ToLowerInvariant(c) : ' ')
        .ToArray();
    return new string(chars).Replace("?", " ").Replace("�", " ").Trim();
}

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
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
    var gameName = "Klondike";
    int? requestedPlayerCount = null;

    if (body is not null)
    {
        if (body.TryGetValue("gameName", out var gameNameEl) && gameNameEl.ValueKind == JsonValueKind.String)
        {
            gameName = gameNameEl.GetString() ?? gameName;
        }
        if (body.TryGetValue("playerCount", out var playerCountEl) && playerCountEl.ValueKind == JsonValueKind.Number && playerCountEl.TryGetInt32(out var parsedPlayerCount))
        {
            requestedPlayerCount = parsedPlayerCount;
        }
    }

    // find matching game in catalog
    var normalizedRequested = NormalizeGameKey(gameName);
    var index = viewModel.VisibleGames.ToList().FindIndex(g =>
        g.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase) ||
        NormalizeGameKey(g.Name) == normalizedRequested);
    if (index < 0)
    {
        return Results.BadRequest(new { message = $"Unknown game: {gameName}" });
    }

    viewModel.MoveSelection(index - viewModel.SelectedGameIndex);
    var playersText = viewModel.VisibleGames[index].Players ?? string.Empty;

    static void AddCandidate(List<int> target, int count)
    {
        if (count > 0 && !target.Contains(count)) target.Add(count);
    }

    var playerCandidates = new List<int>();
    if (requestedPlayerCount.HasValue) AddCandidate(playerCandidates, requestedPlayerCount.Value);
    AddCandidate(playerCandidates, viewModel.ConfiguredPlayerCount);

    var numbers = Regex.Matches(playersText, "\\d+").Select(m => int.Parse(m.Value)).ToList();
    foreach (var n in numbers) AddCandidate(playerCandidates, n);

    if (playersText.Contains('+') && numbers.Count > 0)
    {
        var min = numbers.Min();
        for (var n = min; n <= Math.Max(min + 4, 6); n++) AddCandidate(playerCandidates, n);
    }

    for (var n = 1; n <= 6; n++) AddCandidate(playerCandidates, n);

    var errors = new List<string>();
    foreach (var candidate in playerCandidates)
    {
        try
        {
            var result = viewModel.StartSelectedGame(candidate);
            return Results.Json(new {
                message = result.Message,
                playerCountUsed = candidate,
                undoCount = viewModel.UndoAvailableCount,
                redoCount = viewModel.RedoAvailableCount,
                topRedoLabel = viewModel.TopRedoActionLabel
            });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            errors.Add(ex.Message);
        }
        catch (Exception ex)
        {
            errors.Add(ex.Message);
        }
    }

    return Results.BadRequest(new {
        message = $"Unable to start {gameName} with supported player counts.",
        attemptedPlayerCounts = playerCandidates,
        errors = errors.Distinct().ToList()
    });
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
            pileList = snap.Piles.Select((kv, idx) => new { id = kv.Key, name = kv.Key, index = idx, coords = (pileLayouts.TryGetValue(kv.Key, out var c) ? c : null) }).ToList()
        };
        return Results.Json(data);
});

        app.MapGet("/api/session/actions", () => Results.Json(viewModel.Actions.Select((a, i) => new {
            index = i,
            label = a.Label,
            help = a.HelpText,
            cards = a.Move.Cards,
            expectedSelection = new {
                count = (a.ExpectedSelection?.Count ?? (a.Move.Cards?.Count ?? a.Move.Count)),
                requiresCards = (a.ExpectedSelection?.RequiresCards ?? (a.Move.Cards != null && a.Move.Cards.Count > 0)),
                source = a.ExpectedSelection?.Source ?? a.Move.Source,
                destination = a.ExpectedSelection?.Destination ?? a.Move.Destination,
                allowPartial = a.ExpectedSelection?.AllowPartial ?? false,
                ordered = a.ExpectedSelection?.Ordered ?? false
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
                .Select((a, i) => new { Index = i, Label = a.Label, Cards = a.Move.Cards, Expected = a.ExpectedSelection })
                .Where(x => {
                    if (x.Cards is null || x.Cards.Count == 0) return false;
                    var actionCards = x.Cards.ToList();
                    var selectedSet = new HashSet<string>(selList, StringComparer.OrdinalIgnoreCase);
                    if (x.Expected?.Ordered ?? false)
                    {
                                            // ordered: selection must match sequence exactly (case-insensitive)
                                            return actionCards.SequenceEqual(selList, StringComparer.OrdinalIgnoreCase);
                    }
                    if (x.Expected?.AllowPartial ?? false)
                    {
                                            // allow partial: each selected item must appear in actionCards (case-insensitive)
                                            return selList.All(s => actionCards.Any(ac => string.Equals(ac, s, StringComparison.OrdinalIgnoreCase)));
                    }
                                        // default: exact set equality (case-insensitive)
                    var actionSet = new HashSet<string>(actionCards, StringComparer.OrdinalIgnoreCase);
                    return actionSet.SetEquals(selList);
                })
                .Select(x => new { index = x.Index, label = x.Label })
                .ToList();
            return Results.Json(new { matches });
        });

// Endpoint for client to POST pile layout coords: { pileId: { left, top, width, height } }
app.MapPost("/api/session/pile-layout", async (HttpContext ctx) => {
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,object>>();
    if (body is null) return Results.Json(new { message = "invalid body" });
    foreach (var kv in body)
    {
        pileLayouts[kv.Key] = kv.Value ?? new { };
    }
    return Results.Json(new { message = "ok", count = pileLayouts.Count });
});

// Endpoint for client to POST selected source item rects: { items: [{ title, left, top, width, height }, ...] }
app.MapPost("/api/session/source-layout", async (HttpContext ctx) => {
    var body = await ctx.Request.ReadFromJsonAsync<Dictionary<string,object>>();
    if (body is null || !body.TryGetValue("items", out var itemsObj)) return Results.Json(new { message = "invalid body" });
    try {
        sourceItemsStore.Clear();
        if (itemsObj is JsonElement je && je.ValueKind == System.Text.Json.JsonValueKind.Array)
        {
            foreach (var el in je.EnumerateArray()) sourceItemsStore.Add(el);
        }
        else if (itemsObj is IEnumerable<object> eo)
        {
            foreach (var o in eo) sourceItemsStore.Add(o);
        }
    } catch {
        // best-effort
    }
    return Results.Json(new { message = "ok", count = sourceItemsStore.Count });
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
                                if (index < 0 || index >= viewModel.Actions.Count)
                                {
                                    return Results.BadRequest(new { message = "invalid action index" });
                                }
                                var result = viewModel.ApplySelectedAction(index);
                                // build applied info same as the matches path so client can animate
                                var move = viewModel.Actions[index].Move;
                                object reqSourceItems = null;
                                if (raw.TryGetValue("sourceItems", out var si)) reqSourceItems = si;
                                var appliedInfo = new {
                                    index = index,
                                    label = candidate.Label,
                                    cards = move.Cards,
                                    source = move.Source,
                                    sourcePileId = move.Source,
                                    destination = move.Destination,
                                    destinationPileId = move.Destination,
                                    sourceCoords = (pileLayouts.TryGetValue(move.Source ?? string.Empty, out var sc) ? sc : null),
                                    destinationCoords = (pileLayouts.TryGetValue(move.Destination ?? string.Empty, out var dc) ? dc : null),
                                    sourceItems = reqSourceItems ?? (sourceItemsStore.Count > 0 ? (object)sourceItemsStore : null)
                                };
                                return Results.Json(new { message = result.Message, applied = appliedInfo, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
                            }
                        }

            // Otherwise try to find any action whose Move.Cards match the selection according to ExpectedSelection rules
            var matches = viewModel.Actions
                .Select((a, i) => new { Index = i, Label = a.Label, Cards = a.Move.Cards, Expected = a.ExpectedSelection })
                .Where(x => {
                    if (x.Cards is null || x.Cards.Count == 0) return false;
                    var actionCards = x.Cards.ToList();
                    if (x.Expected?.Ordered ?? false)
                        return actionCards.SequenceEqual(selected, StringComparer.OrdinalIgnoreCase);
                    if (x.Expected?.AllowPartial ?? false)
                        return selected.All(s => actionCards.Any(ac => string.Equals(ac, s, StringComparison.OrdinalIgnoreCase)));
                    var actionSet = new HashSet<string>(actionCards, StringComparer.OrdinalIgnoreCase);
                    return actionSet.SetEquals(selected);
                })
                .ToList();

            if (matches.Count == 1)
            {
                var chosen = matches[0];
                            if (chosen.Index < 0 || chosen.Index >= viewModel.Actions.Count)
                            {
                                return Results.BadRequest(new { message = "invalid action index" });
                            }
                            var result = viewModel.ApplySelectedAction(chosen.Index);
                            var move = viewModel.Actions[chosen.Index].Move;
                                            // attempt to extract sourceItems from request body as fallback
                                            object reqSourceItems = null;
                                            if (raw.TryGetValue("sourceItems", out var si) ) reqSourceItems = si;
                                            // prefer explicit pile ids when Move.Destination looks like a pile key; include both for safety
                                            var appliedInfo = new {
                                                index = chosen.Index,
                                                label = chosen.Label,
                                                            cards = move.Cards ?? Array.Empty<string>(),
                                                            source = move.Source ?? string.Empty,
                                                            sourcePileId = move.Source ?? string.Empty,
                                                            destination = move.Destination ?? string.Empty,
                                                            destinationPileId = move.Destination ?? string.Empty,
                                                // include any client-provided pile layout coords when available
                                                sourceCoords = (pileLayouts.TryGetValue(move.Source ?? string.Empty, out var sc) ? sc : null),
                                                destinationCoords = (pileLayouts.TryGetValue(move.Destination ?? string.Empty, out var dc) ? dc : null),
                                                // echo any sourceItems provided either via prior source-layout post or in this action request
                                                sourceItems = reqSourceItems ?? (sourceItemsStore.Count > 0 ? (object)sourceItemsStore : null)
                                            };
                                                                            Console.WriteLine("APPLIED_INFO: " + System.Text.Json.JsonSerializer.Serialize(appliedInfo));
                                                                            appliedInfoLog.Add(appliedInfo);
                                                                            if (appliedInfoLog.Count > 50) appliedInfoLog.RemoveAt(0);
                                                                            return Results.Json(new { message = result.Message, applied = appliedInfo, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
                        }

            if (matches.Count > 1)
            {
                return Results.Json(new { message = "ambiguous selection", choices = matches.Select(m => new { index = m.Index, label = m.Label }) });
            }

            // Fallback: if selection empty, try applying index-less default action
            if (selected.Count == 0 && index >= 0)
            {
                            if (index < 0 || index >= viewModel.Actions.Count)
                            {
                                return Results.BadRequest(new { message = "invalid action index" });
                            }
                            var result = viewModel.ApplySelectedAction(index);
                            var move = viewModel.Actions[index].Move;
                            object reqSourceItems = null;
                            if (raw.TryGetValue("sourceItems", out var si)) reqSourceItems = si;
                            var appliedInfo = new {
                                index = index,
                                label = viewModel.Actions[index].Label,
                                cards = move.Cards,
                                source = move.Source,
                                sourcePileId = move.Source,
                                destination = move.Destination,
                                destinationPileId = move.Destination,
                                sourceCoords = (pileLayouts.TryGetValue(move.Source ?? string.Empty, out var sc) ? sc : null),
                                destinationCoords = (pileLayouts.TryGetValue(move.Destination ?? string.Empty, out var dc) ? dc : null),
                                sourceItems = reqSourceItems ?? (sourceItemsStore.Count > 0 ? (object)sourceItemsStore : null)
                            };
                            return Results.Json(new { message = result.Message, applied = appliedInfo, undoCount = viewModel.UndoAvailableCount, redoCount = viewModel.RedoAvailableCount, topRedoLabel = viewModel.TopRedoActionLabel });
                        }

            return Results.Json(new { message = "no matching action for selection" });
        });

// debug endpoint to fetch recent appliedInfo entries
app.MapGet("/api/debug/applied-log", () => Results.Json(appliedInfoLog));

app.Run();
