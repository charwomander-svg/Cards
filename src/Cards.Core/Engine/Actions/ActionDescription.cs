using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Actions;

public sealed record ExpectedSelection(int? Count = null, bool RequiresCards = false, bool AllowPartial = false, bool Ordered = false, string? Source = null, string? Destination = null);

public sealed record ActionDescription(MoveDescriptor Move, string Label, string HelpText, ExpectedSelection? ExpectedSelection = null);
