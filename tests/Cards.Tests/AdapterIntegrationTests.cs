using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Cards.Tests
{
    public class AdapterIntegrationTests
    {
        private const string BaseUrl = "http://localhost:5000";

        [Fact]
        public async Task ActionsEndpoint_ReturnsExpectedSelectionStructure()
        {
            using var client = new HttpClient();
            var resp = await client.GetAsync(BaseUrl + "/api/session/actions");
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadFromJsonAsync<object>();
            Assert.NotNull(body);
        }

        [Fact]
        public async Task Snapshot_And_SelectionPreview_WorkTogether()
        {
            using var client = new HttpClient();
            // fetch snapshot
            var snap = await client.GetFromJsonAsync<object>(BaseUrl + "/api/session/snapshot");
            Assert.NotNull(snap);

            // call selection-preview with empty selection (should succeed)
            var previewResp = await client.PostAsJsonAsync(BaseUrl + "/api/session/selection-preview", new { selected = new string[0] });
            previewResp.EnsureSuccessStatusCode();
            var previewBody = await previewResp.Content.ReadFromJsonAsync<object>();
            Assert.NotNull(previewBody);
        }
    }
}
