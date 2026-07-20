namespace Cards.Core.Engine.Autoplay;

public sealed record AutoplayResult(int MovesAttempted, int MovesSucceeded, bool Completed);
