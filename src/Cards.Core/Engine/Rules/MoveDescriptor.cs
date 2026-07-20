using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed record MoveDescriptor(
    string Type,
    string Source,
    string? Destination = null,
    int Count = 1,
    int? PlayerIndex = null,
    int? Bid = null,
    IReadOnlyList<string>? Cards = null,
    Suit? DeclaredSuit = null,
    int? TargetPlayerIndex = null,
    Rank? RequestedRank = null);
