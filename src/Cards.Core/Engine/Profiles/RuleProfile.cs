namespace Cards.Core.Engine.Profiles;

public sealed record RuleProfile(
    string GameName,
    RuleFamily Family,
    int MinPlayers,
    int MaxPlayers,
    int CardsPerPlayer = 0,
    int TableauCount = 0,
    int FoundationCount = 0,
    int DrawCount = 1,
    int TargetTotal = 13,
    bool AlternatingTableau = true,
    bool SupportsTeams = false,
    bool HasBidding = false,
    bool HasMelds = false,
    Scoring.ScorePolicy ScorePolicy = Scoring.ScorePolicy.RankValue,
    int TargetScore = 0);
