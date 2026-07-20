namespace Cards.Xbox;

public sealed record SessionUiSummary(
    string GameName,
    int LegalMoveCount,
    int HandCount,
    int PileCount,
    int? LeadingPlayer,
    int LeadingScore,
    bool IsComplete);
