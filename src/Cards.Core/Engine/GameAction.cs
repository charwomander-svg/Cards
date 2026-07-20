using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine;

public sealed record GameAction(int Sequence, int PlayerIndex, MoveDescriptor Move, MoveResult Result, DateTimeOffset Timestamp);
