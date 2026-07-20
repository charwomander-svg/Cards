using Xunit;
using Cards.Core.Engine.Profiles;

namespace Cards.Tests
{
    public class FallbackMappingsSmokeTests
    {
        [Theory]
        [InlineData("Aces Up")]
        [InlineData("Pyramid")]
        [InlineData("TriPeaks")]
        [InlineData("Golf")]
        [InlineData("Crazy Eights")]
        [InlineData("Uno")]
        [InlineData("President")]
        [InlineData("Gin Rummy")]
        [InlineData("Canasta")]
        public void CreateRules_DoesNotThrow(string gameName)
        {
            var profile = RuleProfileCatalog.ByGameName.ContainsKey(gameName) ? RuleProfileCatalog.ByGameName[gameName] : null;
            if (profile is null)
            {
                // Create a temporary profile
                profile = new RuleProfile(gameName, RuleFamily.Klondike, 1, 1);
            }

            var rules = RuleProfileCatalog.CreateRules(profile);
            Assert.NotNull(rules);
        }
    }
}
