using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Ai;

public sealed class AiMoveSelector
{
    public int Score(MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(move);
        return ScoreMove(move);
    }

    public MoveDescriptor? ChooseMove(IEnumerable<MoveDescriptor> legalMoves)
    {
        ArgumentNullException.ThrowIfNull(legalMoves);

        return legalMoves
            .OrderByDescending(ScoreMove)
            .ThenBy(move => move.Source, StringComparer.OrdinalIgnoreCase)
            .ThenBy(move => move.Destination, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }

    private static int ScoreMove(MoveDescriptor move)
    {
        return move.Type.ToLowerInvariant() switch
        {
            "capture" => 100,
            "clear-pair" => 90,
            "clear-single" => 80,
            "move" when move.Destination?.StartsWith("foundation", StringComparison.OrdinalIgnoreCase) == true => 70,
            "play" => 60,
            "discard" => 50,
            "trail" => 40,
            "move" => 30,
            "deal" => 20,
            "draw" => 10,
            _ => 0
        };
    }
}
