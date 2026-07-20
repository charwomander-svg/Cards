using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Serialization;

public sealed record SavedGameAction(int Sequence, int PlayerIndex, MoveDescriptor Move, bool Succeeded, string Message, DateTimeOffset Timestamp);
