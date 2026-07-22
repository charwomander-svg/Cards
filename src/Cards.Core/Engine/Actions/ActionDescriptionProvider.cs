using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Actions;

public static class ActionDescriptionProvider
{
    public static IReadOnlyList<ActionDescription> Describe(IEnumerable<MoveDescriptor> moves)
    {
        ArgumentNullException.ThrowIfNull(moves);
        return moves.Select(Describe).ToArray();
    }

    public static ActionDescription Describe(MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(move);

        bool requiresCards = move.Cards is not null && move.Cards.Count > 0;
        int? count = move.Cards is not null ? move.Cards.Count : (move.Count > 1 ? move.Count : null);
        bool ordered = move.Type.Contains("sequence", StringComparison.OrdinalIgnoreCase) || move.Type.Equals("move-sequence", StringComparison.OrdinalIgnoreCase);
        bool allowPartial = move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase) || move.Type.Equals("canasta", StringComparison.OrdinalIgnoreCase) || move.Type.Equals("meld-declare", StringComparison.OrdinalIgnoreCase);

        ExpectedSelection expected = new ExpectedSelection(Count: count, RequiresCards: requiresCards, AllowPartial: allowPartial, Ordered: ordered, Source: move.Source, Destination: move.Destination);

        return move.Type.ToLowerInvariant() switch
        {
            "draw" => new ActionDescription(move, $"Draw from {move.Source}", "Take card(s) from the source pile into the active hand or waste.", expected),
            "move" => new ActionDescription(move, $"Move {move.Source} to {move.Destination}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Move the top legal card between piles.", expected),
            "play" => new ActionDescription(move, $"Play {move.Source}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Play the selected card to the active pile.", expected),
            "discard" => new ActionDescription(move, $"Discard {move.Source}", "Discard the selected card and end the turn.", expected),
            "capture" => new ActionDescription(move, $"Capture {move.Destination} with {move.Source}", "Capture a matching table card.", expected),
            "trail" => new ActionDescription(move, $"Trail {move.Source}", "Place a card onto the table without capturing.", expected),
            "clear-single" => new ActionDescription(move, $"Clear {move.Source}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Remove a single card that meets the clearing rule.", expected),
            "clear-pair" => new ActionDescription(move, $"Clear {move.Source} and {move.Destination}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Remove two cards that meet the clearing rule.", expected),
            "deal" => new ActionDescription(move, "Deal to empty spaces", "Refill available empty layout spaces from the stock.", expected),
            "bid" => new ActionDescription(move, $"Bid {move.Bid}", "Make or update the active player's bid.", expected),
            "meld" => new ActionDescription(move, "Meld selected cards", "Declare a scoring set or run from the active player's hand.", expected),
            "pass" => new ActionDescription(move, "Pass", "Skip the active player's action and advance the turn.", expected),
            "knock" => new ActionDescription(move, "Knock", "End the hand using the current score state.", expected),
            _ => new ActionDescription(move, move.Type, "Perform this legal game action.", expected)
        };
    }
}
