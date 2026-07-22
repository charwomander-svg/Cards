using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Core.Engine.Actions;
using Xunit;

namespace Cards.Tests
{
    public class SelectionMatchingTests
    {
        private static bool Matches(ActionDescription action, IReadOnlyList<string> selected)
        {
            var expected = action.ExpectedSelection;
            var actionCards = action.Move.Cards?.ToList() ?? new List<string>();
            if (actionCards.Count == 0) return false;
            if (expected?.Ordered ?? false)
                return actionCards.SequenceEqual(selected, StringComparer.OrdinalIgnoreCase);
            if (expected?.AllowPartial ?? false)
                return selected.All(s => actionCards.Any(ac => string.Equals(ac, s, StringComparison.OrdinalIgnoreCase)));
            var set = new HashSet<string>(actionCards, StringComparer.OrdinalIgnoreCase);
            return set.SetEquals(selected);
        }

        [Fact]
        public void ExactMatch_ReturnsTrue()
        {
            var move = new Cards.Core.Engine.Rules.MoveDescriptor("test", "hand", "target", Cards: new[] { "Ace of Spades", "2 of Hearts" });
            var action = new ActionDescription(move, "label", "help", new ExpectedSelection(Count:2, RequiresCards:true, AllowPartial:false, Ordered:false, Source:move.Source, Destination:move.Destination));
            var selected = new[] { "2 of Hearts", "Ace of Spades" };
            Assert.True(Matches(action, selected));
        }

        [Fact]
        public void PartialMatch_Allowed_ReturnsTrue()
        {
            var move = new Cards.Core.Engine.Rules.MoveDescriptor("meld", "hand", null, Cards: new[] { "Ace of Spades", "2 of Hearts", "3 of Clubs" });
            var action = new ActionDescription(move, "meld", "help", new ExpectedSelection(Count:3, RequiresCards:true, AllowPartial:true, Ordered:false, Source:move.Source, Destination:move.Destination));
            var selected = new[] { "2 of Hearts" };
            Assert.True(Matches(action, selected));
        }

        [Fact]
        public void OrderedMatch_Required_ReturnsTrueOnlyIfOrderMatches()
        {
            var move = new Cards.Core.Engine.Rules.MoveDescriptor("move-sequence", "tableau1", "tableau2", Cards: new[] { "King of Hearts", "Queen of Clubs", "Jack of Diamonds" });
            var action = new ActionDescription(move, "seq", "help", new ExpectedSelection(Count:3, RequiresCards:true, AllowPartial:false, Ordered:true, Source:move.Source, Destination:move.Destination));
            var good = new[] { "King of Hearts", "Queen of Clubs", "Jack of Diamonds" };
            var bad = new[] { "Queen of Clubs", "King of Hearts", "Jack of Diamonds" };
            Assert.True(Matches(action, good));
            Assert.False(Matches(action, bad));
        }
    }
}
