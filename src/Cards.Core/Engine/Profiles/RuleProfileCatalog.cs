using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Profiles;

public static class RuleProfileCatalog
{
    public static IReadOnlyList<RuleProfile> Profiles { get; } = new List<RuleProfile>
    {
        new("Klondike", RuleFamily.Klondike, 1, 1),
        new("Spider", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("FreeCell", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4),
        new("Yukon", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4),
        new("Canfield", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 4, FoundationCount: 4),
        new("Forty Thieves", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("Pyramid", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 28, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("TriPeaks", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 18, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Golf", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 7, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Aces Up", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 4, TargetTotal: 14, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Bridge", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13, SupportsTeams: true, HasBidding: true, ScorePolicy: Scoring.ScorePolicy.TrickCount),
        new("Euchre", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 5),
        new("Pinochle", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 12, SupportsTeams: true, HasBidding: true, HasMelds: true),
        new("Spades", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13, SupportsTeams: true, HasBidding: true),
        new("Hearts", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13),
        new("Whist", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13),
        new("Pitch", RuleFamily.TrickTaking, 2, 6, CardsPerPlayer: 6),
        new("Canasta", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Gin Rummy", RuleFamily.DrawDiscard, 2, 2, CardsPerPlayer: 10),
        new("Rummy", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 7),
        new("Cribbage", RuleFamily.DrawDiscard, 2, 4, CardsPerPlayer: 6),
        new("Thirty-One", RuleFamily.DrawDiscard, 2, 7, CardsPerPlayer: 3),
        new("Crazy Eights", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 5),
        new("Mau Mau", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 5),
        new("Go Fish", RuleFamily.Shedding, 2, 6, CardsPerPlayer: 7),
        new("Casino", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Scopa", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Escoba", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Double Klondike", RuleFamily.Klondike, 1, 1),
        new("Triple Klondike", RuleFamily.Klondike, 1, 1),
        new("Draw One Klondike", RuleFamily.Klondike, 1, 1),
        new("Draw Three Klondike", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4, DrawCount: 3),
        new("Vegas Klondike", RuleFamily.Klondike, 1, 1),
        new("Relaxed Klondike", RuleFamily.Klondike, 1, 1),
        new("Spider One Suit", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("Spider Two Suits", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("Spider Four Suits", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("Spiderette", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4, AlternatingTableau: false),
        new("Scorpion", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4, AlternatingTableau: false),
        new("Double Spider", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8, AlternatingTableau: false),
        new("FreeCell Baker", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4),
        new("Double FreeCell", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 8),
        new("Eight Off", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4),
        new("Baker's Game", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4, AlternatingTableau: false),
        new("Seahaven Towers", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 10, FoundationCount: 4),
        new("Russian Solitaire", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4, AlternatingTableau: false),
        new("Alaska", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4, AlternatingTableau: false),
        new("Australian Patience", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 7, FoundationCount: 4),
        new("Baker's Dozen", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 13, FoundationCount: 4),
        new("Beleaguered Castle", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4),
        new("Flower Garden", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 6, FoundationCount: 4),
        new("Streets and Alleys", RuleFamily.StandardSolitaire, 1, 1, TableauCount: 8, FoundationCount: 4),
        new("Pyramid Relaxed", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 28, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Pyramid Strict", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 28, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Double Pyramid", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 28, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("TriPeaks Open", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 18, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("TriPeaks Closed", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 18, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Golf Relaxed", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 7, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Golf Strict", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 7, TargetTotal: 13, ScorePolicy: Scoring.ScorePolicy.SolitaireCompletion),
        new("Monte Carlo", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 25, TargetTotal: 13),
        new("Accordion", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 52),
        new("Elevens", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 9, TargetTotal: 11),
        new("Fifteens", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 9, TargetTotal: 15),
        new("Fourteen Out", RuleFamily.MatchingSolitaire, 1, 1, TableauCount: 12, TargetTotal: 14),
        new("War", RuleFamily.TrickTaking, 2, 2, CardsPerPlayer: 26),
        new("All Fours", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 6),
        new("All Fives", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 6),
        new("Five Hundred", RuleFamily.TrickTaking, 2, 6, CardsPerPlayer: 10, HasBidding: true),
        new("Oh Hell", RuleFamily.TrickTaking, 2, 7, CardsPerPlayer: 10, HasBidding: true),
        new("Wizard", RuleFamily.TrickTaking, 2, 6, CardsPerPlayer: 10, HasBidding: true),
        new("Sheepshead", RuleFamily.TrickTaking, 3, 5, CardsPerPlayer: 6),
        new("Skat", RuleFamily.TrickTaking, 3, 4, CardsPerPlayer: 10, HasBidding: true),
        new("Belote", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 8, SupportsTeams: true),
        new("Briscola", RuleFamily.TrickTaking, 2, 6, CardsPerPlayer: 3),
        new("Tressette", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 10, SupportsTeams: true),
        new("Tarot", RuleFamily.TrickTaking, 3, 5, CardsPerPlayer: 15),
        new("Contract Bridge", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13, SupportsTeams: true, HasBidding: true, ScorePolicy: Scoring.ScorePolicy.TrickCount),
        new("Duplicate Bridge", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13, SupportsTeams: true, HasBidding: true, ScorePolicy: Scoring.ScorePolicy.TrickCount),
        new("Rubber Bridge", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 13, SupportsTeams: true, HasBidding: true, ScorePolicy: Scoring.ScorePolicy.TrickCount),
        new("Double Deck Pinochle", RuleFamily.TrickTaking, 2, 4, CardsPerPlayer: 12, SupportsTeams: true, HasBidding: true, HasMelds: true),
        new("Cutthroat Pinochle", RuleFamily.TrickTaking, 3, 3, CardsPerPlayer: 15, HasBidding: true, HasMelds: true),
        new("Two-Handed Pinochle", RuleFamily.TrickTaking, 2, 2, CardsPerPlayer: 12, HasBidding: true, HasMelds: true),
        new("Oklahoma Gin", RuleFamily.DrawDiscard, 2, 2, CardsPerPlayer: 10),
        new("Indian Rummy", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 13),
        new("Continental Rummy", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 13),
        new("Shanghai Rummy", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 11),
        new("Contract Rummy", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 10),
        new("Kalooki", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 13),
        new("Conquian", RuleFamily.DrawDiscard, 2, 2, CardsPerPlayer: 10),
        new("Buraco", RuleFamily.DrawDiscard, 2, 4, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Burraco", RuleFamily.DrawDiscard, 2, 4, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Hand and Foot", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Samba", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 15, SupportsTeams: true, HasMelds: true),
        new("Samba Canasta", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 15, SupportsTeams: true, HasMelds: true),
        new("Canasta Bolivia", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Canasta Caliente", RuleFamily.DrawDiscard, 2, 6, CardsPerPlayer: 11, SupportsTeams: true, HasMelds: true),
        new("Uno", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Switch", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Mao", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Cheat", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Old Maid", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("President", RuleFamily.Shedding, 3, 8, CardsPerPlayer: 7),
        new("Palace", RuleFamily.Shedding, 2, 6, CardsPerPlayer: 6),
        new("Sevens", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Fan Tan", RuleFamily.Shedding, 2, 8, CardsPerPlayer: 7),
        new("Durak", RuleFamily.Shedding, 2, 6, CardsPerPlayer: 6),
        new("Big Two", RuleFamily.Shedding, 2, 4, CardsPerPlayer: 13),
        new("Tien Len", RuleFamily.Shedding, 2, 4, CardsPerPlayer: 13),
        new("Dou Dizhu", RuleFamily.Shedding, 3, 3, CardsPerPlayer: 17),
        new("Spite and Malice", RuleFamily.Shedding, 2, 4, CardsPerPlayer: 5),
        new("Casino Draw", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Cassino", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Royal Casino", RuleFamily.Capture, 2, 4, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
        new("Zwickern", RuleFamily.Capture, 2, 8, ScorePolicy: Scoring.ScorePolicy.CaptureCount),
    };

    public static IReadOnlyDictionary<string, RuleProfile> ByGameName { get; } =
        Profiles.ToDictionary(profile => profile.GameName, StringComparer.OrdinalIgnoreCase);

    public static bool TryCreateRules(string gameName, out ICardGameRules rules)
    {
        if (!ByGameName.TryGetValue(gameName, out RuleProfile? profile))
        {
            rules = null!;
            return false;
        }

        rules = CreateRules(profile);
        return true;
    }

    public static ICardGameRules CreateRules(RuleProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

            // Fast-mapping for tutorial-only games: if a profile is present but no dedicated rule exists,
            // attempt to choose a reasonable existing ruleset based on family or common name cues.
            string gameName = profile.GameName;
            return profile.Family switch
            {
            RuleFamily.Klondike when profile.GameName.Equals("Klondike", StringComparison.OrdinalIgnoreCase) => new KlondikeRules(),
            RuleFamily.Klondike => new StandardSolitaireRules(profile.GameName, 7, 4),
            RuleFamily.StandardSolitaire when profile.GameName.Contains("FreeCell", StringComparison.OrdinalIgnoreCase) => new FreeCellRules(profile.GameName),
            RuleFamily.StandardSolitaire when profile.GameName.Contains("Spider", StringComparison.OrdinalIgnoreCase) => new SpiderRules(profile.GameName),
            RuleFamily.StandardSolitaire => new StandardSolitaireRules(profile.GameName, profile.TableauCount, profile.FoundationCount, profile.DrawCount, profile.AlternatingTableau),
            RuleFamily.MatchingSolitaire when profile.GameName.Equals("Accordion", StringComparison.OrdinalIgnoreCase) => new AccordionRules(),
            RuleFamily.MatchingSolitaire => new MatchingSolitaireRules(profile.GameName, profile.TargetTotal, profile.TableauCount),
            RuleFamily.TrickTaking when profile.GameName.Equals("Euchre", StringComparison.OrdinalIgnoreCase) => new EuchreRules(),
            RuleFamily.TrickTaking when profile.GameName.Contains("Bridge", StringComparison.OrdinalIgnoreCase) => new BridgeRules(profile.GameName),
            RuleFamily.TrickTaking when profile.GameName.Contains("Pinochle", StringComparison.OrdinalIgnoreCase) => new PinochleRules(profile.GameName),
            RuleFamily.TrickTaking => new TrickTakingRules(profile.GameName, profile.MinPlayers, profile.MaxPlayers, profile.CardsPerPlayer),
            RuleFamily.DrawDiscard when profile.GameName.Contains("Canasta", StringComparison.OrdinalIgnoreCase) => new CanastaRules(profile.GameName),
            RuleFamily.DrawDiscard when profile.GameName.Contains("Rummy", StringComparison.OrdinalIgnoreCase) || profile.GameName.Contains("Gin", StringComparison.OrdinalIgnoreCase) => new RummyRules(profile.GameName, profile.MinPlayers, profile.MaxPlayers, profile.CardsPerPlayer),
            RuleFamily.DrawDiscard => new DrawDiscardRules(profile.GameName, profile.MinPlayers, profile.MaxPlayers, profile.CardsPerPlayer),
            RuleFamily.Shedding when profile.GameName.Equals("Go Fish", StringComparison.OrdinalIgnoreCase) => new GoFishRules(),
            RuleFamily.Shedding => new SheddingRulesV2(profile.GameName, profile.MinPlayers, profile.MaxPlayers, profile.CardsPerPlayer),
            RuleFamily.Capture => new CasinoRules(profile.GameName),
            _ => CreateFallbackRules(gameName)
        };
    }

    private static ICardGameRules CreateFallbackRules(string gameName)
    {
        // Simple heuristics based on game name and catalog classification to return an existing rules implementation
        if (gameName.Contains("solitaire", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("klondike", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("spider", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("freecell", StringComparison.OrdinalIgnoreCase))
        {
            return new StandardSolitaireRules(gameName, 7, 4);
        }

        if (gameName.Contains("pyramid", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("tripeaks", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("golf", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("aces up", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("monte carlo", StringComparison.OrdinalIgnoreCase))
        {
            return new MatchingSolitaireRules(gameName, 13, 28);
        }

        if (gameName.Contains("crazy", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("uno", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("president", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("big two", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("mau mau", StringComparison.OrdinalIgnoreCase))
        {
            return new SheddingRulesV2(gameName, 2, 8, 5);
        }

        if (gameName.Contains("rummy", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("gin", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("canasta", StringComparison.OrdinalIgnoreCase))
        {
            return new RummyRules(gameName, 2, 6, 10);
        }

        if (gameName.Contains("euchre", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("pinochle", StringComparison.OrdinalIgnoreCase) ||
            gameName.Contains("bridge", StringComparison.OrdinalIgnoreCase))
        {
            return new TrickTakingRules(gameName, 2, 4, 13);
        }

        // Fallback to a simple draw/discard ruleset
        return new DrawDiscardRules(gameName, 2, 6, 5);
    }
}
