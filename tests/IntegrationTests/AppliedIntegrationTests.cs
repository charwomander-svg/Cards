using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class AppliedIntegrationTests
    {
        private readonly HttpClient _http = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        [Fact]
        public async Task ActionEndpoint_ShouldReturnApplied_WithSourceItemsAndDestinationCoords()
        {
            // Only run when RUN_INTEGRATION env var is set to '1' (CI will set this)
            var runIntegration = Environment.GetEnvironmentVariable("RUN_INTEGRATION");
            if (string.IsNullOrEmpty(runIntegration) || runIntegration != "1")
            {
                // Early exit so the test passes quickly when not running in CI
                return;
            }
            // Start a Klondike game
            var startBody = JsonSerializer.Serialize(new { gameName = "Klondike" });
            await _http.PostAsync("/api/session/start", new StringContent(startBody, Encoding.UTF8, "application/json"));

            // get actions and snapshot
            var actionsResp = await _http.GetStringAsync("/api/session/actions");
            var actions = JsonDocument.Parse(actionsResp).RootElement;

            var snapResp = await _http.GetStringAsync("/api/session/snapshot");
            var snap = JsonDocument.Parse(snapResp).RootElement;

            // find first action with cards and a card present in a pile
            int actionIndex = -1;
            string cardTitle = null;
            foreach (var a in actions.EnumerateArray())
            {
                if (!a.TryGetProperty("cards", out var cards) || cards.GetArrayLength() == 0) continue;
                var c = cards[0].GetString();
                // search piles
                if (snap.TryGetProperty("piles", out var piles))
                {
                    foreach (var prop in piles.EnumerateObject())
                    {
                        foreach (var ct in prop.Value.EnumerateArray())
                        {
                            if (ct.GetString() == c)
                            {
                                actionIndex = a.GetProperty("index").GetInt32();
                                cardTitle = c;
                                break;
                            }
                        }
                        if (cardTitle != null) break;
                    }
                }
                if (cardTitle != null) break;
            }

            Assert.True(actionIndex >= 0, "No candidate action found with card in a pile");

            // post pile-layout for all piles (from snapshot.pileList)
            if (snap.TryGetProperty("pileList", out var pileList))
            {
                var pl = new System.Dynamic.ExpandoObject() as System.Collections.Generic.IDictionary<string, object>;
                foreach (var p in pileList.EnumerateArray())
                {
                    var id = p.GetProperty("id").GetString();
                    if (p.TryGetProperty("coords", out var coords))
                    {
                        pl[id] = new { left = coords.GetProperty("left").GetInt32(), top = coords.GetProperty("top").GetInt32(), width = coords.GetProperty("width").GetInt32(), height = coords.GetProperty("height").GetInt32() };
                    }
                }
                var plJson = JsonSerializer.Serialize(pl);
                await _http.PostAsync("/api/session/pile-layout", new StringContent(plJson, Encoding.UTF8, "application/json"));
            }

            // post source-layout for the selected card
            var si = new[] { new { title = cardTitle, left = 120, top = 60, width = 80, height = 110 } };
            var siJson = JsonSerializer.Serialize(new { items = si });
            await _http.PostAsync("/api/session/source-layout", new StringContent(siJson, Encoding.UTF8, "application/json"));

            // preview
            var previewBody = JsonSerializer.Serialize(new { selected = new[] { cardTitle } });
            var previewResp = await _http.PostAsync("/api/session/selection-preview", new StringContent(previewBody, Encoding.UTF8, "application/json"));
            var previewJson = await previewResp.Content.ReadAsStringAsync();

            int chosenIndex = actionIndex;
            try
            {
                using var doc = JsonDocument.Parse(previewJson);
                if (doc.RootElement.TryGetProperty("matches", out var matches) && matches.GetArrayLength() > 0)
                {
                    chosenIndex = matches[0].GetProperty("index").GetInt32();
                }
            }
            catch { }

            var actionBody = JsonSerializer.Serialize(new { index = chosenIndex, selected = new[] { cardTitle }, sourceItems = si });
            var actionResp = await _http.PostAsync("/api/session/action", new StringContent(actionBody, Encoding.UTF8, "application/json"));
            var actionJson = await actionResp.Content.ReadAsStringAsync();

            using var actionDoc = JsonDocument.Parse(actionJson);
            Assert.True(actionDoc.RootElement.TryGetProperty("applied", out var applied), "Response did not include applied object");
            Assert.True(applied.TryGetProperty("sourceItems", out var sitems) && sitems.GetArrayLength() > 0, "applied.sourceItems missing or empty");
            Assert.True(applied.TryGetProperty("destinationCoords", out var dcoords), "applied.destinationCoords missing");
        }
    }
}
