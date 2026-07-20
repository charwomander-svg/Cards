namespace Cards.Core.Engine.Rules;

public interface ICardGameRules
{
    string Name { get; }
    int MinPlayers { get; }
    int MaxPlayers { get; }
    RuleCapabilities Capabilities => new(
        Name,
        MinPlayers,
        MaxPlayers,
        SupportsTeams: false,
        HasHiddenInformation: MaxPlayers > 1,
        HasStock: true,
        HasTableau: false,
        HasBidding: false,
        HasMelds: false,
        HasCaptures: false,
        HasTricks: false,
        CanDraw: true,
        CanDiscard: false,
        MoveTypes: Array.Empty<string>());

    CardGameSession CreateSession(int playerCount, Random? random = null);
    IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session);
    MoveResult ApplyMove(CardGameSession session, MoveDescriptor move);
}
