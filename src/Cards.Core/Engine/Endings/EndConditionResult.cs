namespace Cards.Core.Engine.Endings;

public sealed record EndConditionResult(bool IsMet, EndCondition? Condition, string Reason)
{
    public static EndConditionResult NotMet { get; } = new(false, null, "No end condition has been met.");
}
