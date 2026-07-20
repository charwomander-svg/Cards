namespace Cards.Core.Engine;

public sealed record MoveResult(bool Succeeded, string Message)
{
    public static MoveResult Success(string message = "Move completed.") => new(true, message);
    public static MoveResult Failure(string message) => new(false, message);
}
