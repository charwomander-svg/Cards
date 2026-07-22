using Cards.Core.Engine.Rules;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Validation;
using Cards.Core.Engine.Endings;
using Cards.Core.Engine.Rounds;

namespace Cards.Core.Engine;

public sealed class CardGameEngine
{
    private readonly Dictionary<string, ICardGameRules> _rules = new(StringComparer.OrdinalIgnoreCase);

    public CardGameEngine(IEnumerable<ICardGameRules>? rules = null)
    {
        foreach (RuleProfile profile in RuleProfileCatalog.Profiles)
            Register(RuleProfileCatalog.CreateRules(profile));

        // Fast-register fallback rules for any catalog games that don't have a dedicated profile
        foreach (var game in Cards.Core.Data.CardGameCatalog.Games)
        {
            if (!_rules.ContainsKey(game.Name))
            {
                var profile = RuleProfileCatalog.ByGameName.ContainsKey(game.Name)
                    ? RuleProfileCatalog.ByGameName[game.Name]
                    : new RuleProfile(game.Name, RuleFamily.Klondike, 1, 1);
                Register(RuleProfileCatalog.CreateRules(profile));
            }
        }

        if (rules is not null)
            foreach (ICardGameRules ruleSet in rules)
                Register(ruleSet);
    }

    public IReadOnlyCollection<string> SupportedGames => _rules.Keys.ToArray();
    public IReadOnlyCollection<RuleProfile> SupportedProfiles => RuleProfileCatalog.Profiles;

    public void Register(ICardGameRules rules)
    {
        ArgumentNullException.ThrowIfNull(rules);
        _rules[rules.Name] = rules;
    }

    public CardGameSession StartGame(string gameName, int playerCount, Random? random = null)
    {
        ICardGameRules rules = GetRules(gameName);
        int seed;
        Random rng;
        if (random is null)
        {
            seed = new Random().Next();
            rng = new Random(seed);
        }
        else
        {
            // If caller provided a Random, try to capture a reproducible seed by taking the next int.
            seed = random.Next();
            rng = new Random(seed);
        }

        CardGameSession session = rules.CreateSession(playerCount, rng);
        // Preserve seed for deterministic replay/undo.
        session.Seed = seed;
        return session;
    }

    // Start a game using an explicit seed (useful for deterministic replay)
    public CardGameSession StartGameWithSeed(string gameName, int playerCount, int seed)
    {
        ICardGameRules rules = GetRules(gameName);
        Random rng = new Random(seed);
        CardGameSession session = rules.CreateSession(playerCount, rng);
        session.Seed = seed;
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        return GetRules(session.GameName).GetLegalMoves(session);
    }

    public RuleCapabilities GetCapabilities(string gameName) => GetRules(gameName).Capabilities;
    public ValidationResult ValidateSession(CardGameSession session) => new SessionValidator(this).ValidateSession(session);
    public ValidationResult ValidateMove(CardGameSession session, MoveDescriptor move) => new SessionValidator(this).ValidateMove(session, move);
    public EndConditionResult EvaluateEndConditions(CardGameSession session, int targetScore = 0) => new EndConditionEvaluator(this).Evaluate(session, targetScore);
    public RoundSummary BuildRoundSummary(CardGameSession session, int targetScore = 0) => new RoundSummaryBuilder(this).Build(session, targetScore);

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);
        int playerIndex = move.PlayerIndex ?? session.CurrentPlayerIndex;
        MoveResult result = IsGenericAction(move)
            ? session.ApplyGenericAction(move)
            : GetRules(session.GameName).ApplyMove(session, move);
        session.RecordAction(playerIndex, move, result);
        return result;
    }

    private static bool IsGenericAction(MoveDescriptor move)
    {
        return move.Type.Equals("bid", StringComparison.OrdinalIgnoreCase)
            || move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase)
            || move.Type.Equals("pass", StringComparison.OrdinalIgnoreCase)
            || move.Type.Equals("knock", StringComparison.OrdinalIgnoreCase);
    }

    private ICardGameRules GetRules(string gameName)
    {
        if (!_rules.TryGetValue(gameName, out ICardGameRules? rules))
            throw new KeyNotFoundException($"No rule engine registered for '{gameName}'.");

        return rules;
    }
}
