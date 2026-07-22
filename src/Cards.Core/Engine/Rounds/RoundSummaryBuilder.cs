using Cards.Core.Engine.Endings;

namespace Cards.Core.Engine.Rounds;

public sealed class RoundSummaryBuilder
{
    private readonly EndConditionEvaluator _endConditionEvaluator;

    public RoundSummaryBuilder(CardGameEngine engine)
    {
        _endConditionEvaluator = new EndConditionEvaluator(engine ?? throw new ArgumentNullException(nameof(engine)));
    }

    public RoundSummary Build(CardGameSession session, int targetScore = 0)
    {
        ArgumentNullException.ThrowIfNull(session);
        EndConditionResult endCondition = _endConditionEvaluator.Evaluate(session, targetScore);
        if (endCondition.IsMet)
            session.IsComplete = true;

        return new RoundSummary(
            session.GameName,
            session.Round,
            session.IsComplete,
            session.WinnerIndex,
            session.Scores.ToDictionary(pair => pair.Key, pair => pair.Value),
            session.TeamScores.ToDictionary(pair => pair.Key, pair => pair.Value),
            endCondition,
            session.CreateStatisticsSnapshot());
    }
}
