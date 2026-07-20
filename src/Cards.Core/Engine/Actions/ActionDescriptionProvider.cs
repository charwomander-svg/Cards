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

        return move.Type.ToLowerInvariant() switch
        {
            "draw" => new ActionDescription(move, $"Draw from {move.Source}", "Take card(s) from the source pile into the active hand or waste."),
            "move" => new ActionDescription(move, $"Move {move.Source} to {move.Destination}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Move the top legal card between piles."),
            "play" => new ActionDescription(move, $"Play {move.Source}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Play the selected card to the active pile."),
            "discard" => new ActionDescription(move, $"Discard {move.Source}", "Discard the selected card and end the turn."),
            "capture" => new ActionDescription(move, $"Capture {move.Destination} with {move.Source}", "Capture a matching table card."),
            "trail" => new ActionDescription(move, $"Trail {move.Source}", "Place a card onto the table without capturing."),
            "clear-single" => new ActionDescription(move, $"Clear {move.Source}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Remove a single card that meets the clearing rule."),
            "clear-pair" => new ActionDescription(move, $"Clear {move.Source} and {move.Destination}{(move.Cards is not null ? $" ({string.Join(", ", move.Cards)})" : string.Empty)}", "Remove two cards that meet the clearing rule."),
            "deal" => new ActionDescription(move, "Deal to empty spaces", "Refill available empty layout spaces from the stock."),
            "bid" => new ActionDescription(move, $"Bid {move.Bid}", "Make or update the active player's bid."),
            "meld" => new ActionDescription(move, "Meld selected cards", "Declare a scoring set or run from the active player's hand."),
            "pass" => new ActionDescription(move, "Pass", "Skip the active player's action and advance the turn."),
            "knock" => new ActionDescription(move, "Knock", "End the hand using the current score state."),
            _ => new ActionDescription(move, move.Type, "Perform this legal game action.")
        };
    }
}
