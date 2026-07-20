using Cards.Core.Engine.Actions;
using Cards.Core.Engine.Ai;

namespace Cards.Core.Engine.Hints;

public sealed class HintProvider
{
    private readonly CardGameEngine _engine;
    private readonly AiMoveSelector _selector;

    public HintProvider(CardGameEngine engine, AiMoveSelector? selector = null)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        _selector = selector ?? new AiMoveSelector();
    }

    public IReadOnlyList<Hint> GetHints(CardGameSession session, int maxHints = 3)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (maxHints < 1)
            throw new ArgumentOutOfRangeException(nameof(maxHints), "At least one hint is required.");

        return _engine.GetLegalMoves(session)
            .Select(move => new Hint(move, ActionDescriptionProvider.Describe(move), _selector.Score(move)))
            .OrderByDescending(hint => hint.Score)
            .ThenBy(hint => hint.Description.Label, StringComparer.OrdinalIgnoreCase)
            .Take(maxHints)
            .ToArray();
    }
}
