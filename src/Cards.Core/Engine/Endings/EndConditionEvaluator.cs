namespace Cards.Core.Engine.Endings;

public sealed class EndConditionEvaluator
{
    private readonly CardGameEngine _engine;

    public EndConditionEvaluator(CardGameEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    public EndConditionResult Evaluate(CardGameSession session, int targetScore = 0)
    {
        ArgumentNullException.ThrowIfNull(session);

        int foundationCards = session.Piles.Values.Where(pile => pile.Name.StartsWith("foundation", StringComparison.OrdinalIgnoreCase)).Sum(pile => pile.Count);
        if (foundationCards == 52)
            return new EndConditionResult(true, EndCondition.FoundationComplete, "All cards are on foundations.");

        if (targetScore > 0 && (session.Scores.Values.Any(score => score >= targetScore) || session.TeamScores.Values.Any(score => score >= targetScore)))
            return new EndConditionResult(true, EndCondition.TargetScore, $"A score reached the target of {targetScore}.");

        if (session.Piles.TryGetValue("stock", out CardPile? stock) && stock.IsEmpty)
            return new EndConditionResult(true, EndCondition.StockEmpty, "The stock is empty.");

        if (session.Hands.Any(hand => hand.IsEmpty) && session.PlayerCount > 1)
            return new EndConditionResult(true, EndCondition.EmptyHand, "At least one player has emptied their hand.");

        if (_engine.GetLegalMoves(session).Count == 0)
            return new EndConditionResult(true, EndCondition.NoLegalMoves, "No legal moves are available.");

        return EndConditionResult.NotMet;
    }
}
