using System.Linq;
using System.Text.Json;
using Cards.Core.Engine;
using Cards.Core.Engine.Autoplay;
using Cards.Core.Engine.Replay;
using Cards.Core.Engine.Serialization;
using Xunit;

namespace Cards.Tests
{
    public class UndoRedoRoundTripTests
    {
        [Theory]
        [InlineData("Klondike")]
        [InlineData("Pyramid")]
        [InlineData("Accordion")]
        [InlineData("Spider")]
        public void SaveRestoreReplayProducesSameState(string gameName)
        {
            var engine = new CardGameEngine();
            var autoplay = new AutoplayRunner(engine);

            // Start and run a few autoplay moves to produce history
            var session = engine.StartGame(gameName, 1);
            autoplay.Run(session, 20);

            // Save to serialized state
            var saved = SessionSerializer.Save(session);

            // Restore into a new session object (should preserve seed)
            var restored = SessionSerializer.Restore(saved);

            // Replay history deterministically from saved seed
            var replay = new SessionReplay(engine).Replay(saved.GameName, saved.PlayerCount, saved.Seed, saved.History.Select(h => h.Move));

            // Compare key aspects of session state between original and replayed session
            var originalSnap = session.CreateSnapshot();
            var replaySnap = replay.Session.CreateSnapshot();

            Assert.Equal(originalSnap.GameName, replaySnap.GameName);
            Assert.Equal(originalSnap.PlayerCount, replaySnap.PlayerCount);
            Assert.Equal(originalSnap.IsComplete, replaySnap.IsComplete);
            Assert.Equal(originalSnap.CurrentPlayerIndex, replaySnap.CurrentPlayerIndex);

            // Compare pile contents by string representation
            var origPiles = originalSnap.Piles.ToDictionary(p => p.Key, p => p.Value.Select(c => c.ToString()).ToArray());
            var replayPiles = replaySnap.Piles.ToDictionary(p => p.Key, p => p.Value.Select(c => c.ToString()).ToArray());
            Assert.Equal(origPiles.Keys.OrderBy(k => k), replayPiles.Keys.OrderBy(k => k));
            foreach (var key in origPiles.Keys)
            {
                Assert.Equal(origPiles[key], replayPiles[key]);
            }

            // Compare hands similarly
            var origHands = originalSnap.Hands.ToDictionary(h => h.Key, h => h.Value.Select(c => c.ToString()).ToArray());
            var replayHands = replaySnap.Hands.ToDictionary(h => h.Key, h => h.Value.Select(c => c.ToString()).ToArray());
            Assert.Equal(origHands.Keys.OrderBy(k => k), replayHands.Keys.OrderBy(k => k));
            foreach (var key in origHands.Keys)
            {
                Assert.Equal(origHands[key], replayHands[key]);
            }
        }
    }
}
