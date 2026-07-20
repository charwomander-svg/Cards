using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Replay;

public sealed class SessionReplay
{
    private readonly CardGameEngine _engine;

    public SessionReplay(CardGameEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    public ReplayResult Replay(string gameName, int playerCount, int seed, IEnumerable<MoveDescriptor> moves)
    {
        ArgumentNullException.ThrowIfNull(moves);

        CardGameSession session = _engine.StartGameWithSeed(gameName, playerCount, seed);
        var results = new List<MoveResult>();
        foreach (MoveDescriptor move in moves)
            results.Add(_engine.ApplyMove(session, move));

        return new ReplayResult(session, results);
    }

    public ReplayResult UndoLast(CardGameSession session, int seed)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (session.History.Count == 0)
            return new ReplayResult(session, Array.Empty<MoveResult>());

        return Replay(
            session.GameName,
            session.PlayerCount,
            seed,
            session.History.Take(session.History.Count - 1).Select(action => action.Move));
    }
}
