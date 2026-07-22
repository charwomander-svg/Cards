using Cards.Core.Engine.Ai;
using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Autoplay;

public sealed class AutoplayRunner
{
    private readonly CardGameEngine _engine;
    private readonly AiMoveSelector _selector;

    public AutoplayRunner(CardGameEngine engine, AiMoveSelector? selector = null)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        _selector = selector ?? new AiMoveSelector();
    }

    public MoveResult Step(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        MoveDescriptor? move = _selector.ChooseMove(_engine.GetLegalMoves(session));
        return move is null ? MoveResult.Failure("No legal moves are available.") : _engine.ApplyMove(session, move);
    }

    public AutoplayResult Run(CardGameSession session, int maxMoves)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (maxMoves < 1)
            throw new ArgumentOutOfRangeException(nameof(maxMoves), "At least one move is required.");

        int attempted = 0;
        int succeeded = 0;
        while (!session.IsComplete && attempted < maxMoves)
        {
            MoveResult result = Step(session);
            attempted++;
            if (!result.Succeeded)
                break;

            succeeded++;
        }

        return new AutoplayResult(attempted, succeeded, session.IsComplete);
    }
}
