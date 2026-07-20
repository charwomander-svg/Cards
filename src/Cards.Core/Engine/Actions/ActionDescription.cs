using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Actions;

public sealed record ActionDescription(MoveDescriptor Move, string Label, string HelpText);
