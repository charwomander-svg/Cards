using Cards.Core.Engine.Actions;
using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Hints;

public sealed record Hint(MoveDescriptor Move, ActionDescription Description, int Score);
