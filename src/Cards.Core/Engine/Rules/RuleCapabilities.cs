namespace Cards.Core.Engine.Rules;

public sealed record RuleCapabilities(
    string Name,
    int MinPlayers,
    int MaxPlayers,
    bool SupportsTeams,
    bool HasHiddenInformation,
    bool HasStock,
    bool HasTableau,
    bool HasBidding,
    bool HasMelds,
    bool HasCaptures,
    bool HasTricks,
    bool CanDraw,
    bool CanDiscard,
    IReadOnlyList<string> MoveTypes);
