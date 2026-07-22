namespace Cards.Core.Engine.Replay;

public sealed record ReplayResult(CardGameSession Session, IReadOnlyList<MoveResult> Results);
