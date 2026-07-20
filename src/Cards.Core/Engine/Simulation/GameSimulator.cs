namespace Cards.Core.Engine.Simulation;

public sealed class GameSimulator
{
    private readonly CardGameEngine _engine;

    public GameSimulator(CardGameEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    public SimulationResult Run(string gameName, int playerCount, int seed, int maxMoves)
    {
        if (maxMoves < 1)
            throw new ArgumentOutOfRangeException(nameof(maxMoves), "Simulation needs at least one move.");

        var random = new Random(seed);
        CardGameSession session = _engine.StartGame(gameName, playerCount, new Random(seed));
        int attempted = 0;
        int succeeded = 0;

        while (!session.IsComplete && attempted < maxMoves)
        {
            IReadOnlyList<Rules.MoveDescriptor> legalMoves = _engine.GetLegalMoves(session);
            if (legalMoves.Count == 0)
                break;

            Rules.MoveDescriptor move = legalMoves[random.Next(legalMoves.Count)];
            MoveResult result = _engine.ApplyMove(session, move);
            attempted++;
            if (result.Succeeded)
                succeeded++;
        }

        return new SimulationResult(
            session.GameName,
            attempted,
            succeeded,
            session.IsComplete,
            session.WinnerIndex,
            session.CreateStatisticsSnapshot());
    }
}
