using System.Collections.Generic;

namespace Cards.Core.Data;

/// <summary>
/// Catalog of card games and variations with compact tutorial rule sets.
/// </summary>
public static class CardGameCatalog
{
    public static IReadOnlyList<CardGameStyleGroup> StyleGroups { get; } = new List<CardGameStyleGroup>
    {
        new("Builder Solitaire", "Single-player games built around tableau columns and foundations.", new[]
        {
            "Klondike", "FreeCell", "Yukon", "Canfield", "Eight Off", "Baker's Game", "Seahaven Towers", "Russian Solitaire", "Alaska", "Westcliff", "Bisley", "Double Klondike", "Triple Klondike", "Beleaguered Castle", "Flower Garden", "Streets and Alleys", "Demon", "Josephine", "Limited", "Australian Patience", "Batsford", "Carlton", "Chinese Solitaire", "Emperor", "King Albert", "Raglan", "Tournament", "Waterloo", "Zebra", "Castles in Spain", "Spanish Castle", "Relaxed FreeCell", "Relaxed Klondike", "Vegas Klondike", "Draw One Klondike", "Draw Three Klondike", "Thoughtful Solitaire", "Whitehead", "Yukon Cells", "Double FreeCell", "Yukon Russian", "Yukon Alaska", "Canfield Storehouse", "Canfield Rainbow", "Eight Off Relaxed", "Eight Off Strict",
        }),
        new("Spider-Style Solitaire", "Single-player games focused on building packed in-suit or descending sequences across many tableau piles.", new[]
        {
            "Spider", "Spiderette", "Wasp", "Will o' the Wisp", "Scorpion", "Forty Thieves", "Simple Simon", "Little Spider", "Double Scorpion", "Double Spider", "Triple Spider", "Quadruple Spider", "Spider One Suit", "Spider Two Suits", "Spider Four Suits", "Forty Thieves Open", "Forty Thieves Strict", "Scorpion Easy", "Scorpion Hard", "Big Forty", "Little Forty",
        }),
        new("Pairing and Clearing Solitaire", "Single-player games where cards are removed by pairs, totals, ranks, or layout-clearing goals.", new[]
        {
            "Pyramid", "TriPeaks", "Golf", "Aces Up", "Accordion", "Clock Solitaire", "Monte Carlo", "Fourteen Out", "Black Hole", "Eliminator", "Nestor", "Gaps", "Montana", "Good Thirteen", "Triple Peaks", "Addiction", "Maze", "Cribbage Solitaire", "Decade", "Four Leaf Clovers", "Pairs", "Fifteen Puzzle Solitaire", "Block Eleven", "Bowling Solitaire", "Elevens", "Fifteens", "Hit or Miss", "Pairing", "Patient Pairs", "Suit Elevens", "Take Fourteen", "Ten Across", "Wish", "Double Pyramid", "Double Golf", "Double TriPeaks", "Pyramid Relaxed", "Pyramid Strict", "TriPeaks Open", "TriPeaks Closed", "Golf Relaxed", "Golf Strict", "Aces Up Relaxed", "Aces Up Strict", "Monte Carlo Thirteens", "Monte Carlo Same Rank", "Clock Patience", "Clock Open", "Accordion Easy", "Accordion Strict",
        }),
        new("Reserve and Open-Cell Solitaire", "Single-player games with reserves, cells, fans, or special open holding areas.", new[]
        {
            "La Belle Lucie", "Penguin", "Cruel", "Osmosis", "Peek", "Trefoil", "Terrace", "Reserve", "Storehouse", "Crescent", "Duchess", "Eagle Wing", "Glenwood", "Gold Mine", "Grand Duchess", "Lucas", "Milligan Cell", "Napoleon's Tomb", "Pasha", "Patriarchs", "Reserves", "Saratoga", "Shamrocks", "Stalactites", "Three Shuffles and a Draw", "Thumb and Pouch", "Open Crescent", "Puss in the Corner", "Quadrille", "Royal Parade", "Sultan", "Carpet", "La Belle Lucie Fan", "La Belle Lucie Three Shuffles", "Cruel Redeal", "Cruel No Redeal", "Penguin Open", "Penguin Cells",
        }),
        new("Pattern and Layout Solitaire", "Single-player patience games organized around named shapes, clocks, castles, squares, wheels, or unusual tableau patterns.", new[]
        {
            "Calculation", "Four Seasons", "Windmill", "Napoleon at St Helena", "Rouge et Noir", "Royal Cotillion", "Matrimony", "Napoleon's Square", "Parliament", "Salic Law", "Vanishing Cross", "Zodiac", "Alternations", "Capricieuse", "Colorado", "Congress", "Diplomat", "Fortress", "Labyrinth", "Lady Jane", "Martha", "Miss Milligan", "Queen of Italy", "Rank and File", "Red and Black", "Royal Marriage", "Somerset", "Spanish Patience", "Thieves of Egypt", "Virginia Reel", "Aunt Mary", "Archway", "Assembly", "Auld Lang Syne", "Betsy Ross", "Big Ben", "Blockade", "Boulevard", "Box Kite", "Camelot", "Captive Queens", "Chessboard", "Corner Card", "Courtyard", "Great Wheel", "Herringbone", "Long Braid", "Mount Olympus", "Rivers and Lakes", "Union Square", "Wave Motion", "Windsor Castle", "Xerxes", "Zigzag",
        }),
        new("Trick-Taking", "Player-vs-player games where players lead, follow suit, capture tricks, bid, or score card points.", new[]
        {
            "Bridge", "Contract Bridge", "Duplicate Bridge", "Rubber Bridge", "Honeymoon Bridge", "Whist", "Solo Whist", "Bid Whist", "Knockout Whist", "German Whist", "Danish Whist", "Euchre", "Five Hundred", "Oh Hell", "Oh Pshaw", "Wizard", "Spades", "Hearts", "Black Maria", "Cancellation Hearts", "Omnibus Hearts", "Spot Hearts", "Pinochle", "Pinochle Auction", "Double Deck Pinochle", "Cutthroat Pinochle", "Two-Handed Pinochle", "Pitch", "Auction Pitch", "High-Low-Jack", "Pedro", "Smear", "Setback", "Sheepshead", "Skat", "Belote", "Belote Coinchée", "Coinche", "Sueca", "Tute", "Briscola", "Briscola Chiamata", "Tressette", "Tresette", "Tarot", "Tarneeb", "Baloot", "Preferans", "Doppelkopf", "Schafkopf", "Watten", "Sixty-Six", "Schnapsen", "Marriage", "Mariáš", "Jass", "Klaberjass", "Klaverjas", "Nap", "Boston", "Cinch", "Clabber", "Court Piece", "Rang", "Hokm", "Barbu",
        }),
        new("Rummy and Meld-Building", "Player-vs-player games centered on drawing, discarding, melding sets/runs, and going out.", new[]
        {
            "Rummy", "Gin Rummy", "Indian Rummy", "Canasta", "Canasta Bolivia", "Canasta Caliente", "Samba Canasta", "Samba", "Buraco", "Burraco", "Conquian", "Continental Rummy", "Kalooki", "Manipulation Rummy", "Oklahoma Gin", "Panguingue", "Shanghai Rummy", "Dummy Rummy", "Liverpool Rummy", "Contract Rummy", "May I", "Hand and Foot", "Continental", "Carioca", "Scala Quaranta", "Chinchón", "Tong-its",
        }),
        new("Shedding and Matching PvP", "Player-vs-player games where the main goal is to empty the hand, match rank/suit, or avoid being left with cards.", new[]
        {
            "Crazy Eights", "Mau Mau", "Uno", "Switch", "Mao", "Go Fish", "Old Maid", "Cheat", "President", "President Asshole", "Palace", "Kings in the Corner", "Kings Corners", "Fan Tan", "Sevens", "Spoons", "Slapjack", "Egyptian Ratscrew", "Beggar-my-neighbor", "War", "Speed", "Spit", "California Speed", "Durak", "Durak Podkidnoy", "Durak Perevodnoy", "Daifugo", "Big Two", "Dou Dizhu", "Tien Len", "Fight the Landlord", "Pusoy Dos", "Zheng Fen", "Skip-Bo", "Spite and Malice", "Phase 10", "Dutch Blitz", "Ligretto", "The Great Dalmuti", "Tichu", "Haggis",
        }),
        new("Fishing and Capture", "Player-vs-player games where players capture table cards, make point-scoring combinations, or collect specific cards.", new[]
        {
            "Casino", "Cassino", "Cassino Royal", "Cassino Spade", "Casino Draw", "Royal Casino", "Scopa", "Scopone", "Escoba", "Escoba de 15", "Zwickern", "All Fours", "All Fives", "Seven Up Solitaire",
        }),
        new("Banking, Gambling, and Poker", "Player-vs-player wagering, banking, comparing, and poker-family games.", new[]
        {
            "Blackjack", "Faro", "Baccarat", "Red Dog", "In-Between", "Acey Deucey", "Brag", "Teen Patti", "Three Card Poker", "Texas Hold'em", "Omaha", "Seven Card Stud", "Five Card Draw", "Lowball", "Badugi", "Commerce", "Pope Joan", "Newmarket", "Michigan", "Lanterloo", "Loo", "Écarté", "Booray", "Thirty-One", "Scat", "Tonk", "Golf Card Game", "Cambio", "Cabo", "Mus",
        }),
    };

    public static IReadOnlyDictionary<string, CardGameStyleGroup> StyleGroupByGame { get; } = StyleGroups
        .SelectMany(group => group.GameNames.Select(game => new KeyValuePair<string, CardGameStyleGroup>(game, group)))
        .ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<CardGameRuleSet> Games { get; } = new List<CardGameRuleSet>
    {
        new("Klondike", "Solitaire", "1", "Complete Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Spider", "Solitaire", "1", "Complete Spider by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("FreeCell", "Solitaire", "1", "Complete FreeCell by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Pyramid", "Solitaire", "1", "Complete Pyramid by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("TriPeaks", "Solitaire", "1", "Complete TriPeaks by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Golf", "Solitaire", "1", "Complete Golf by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Canfield", "Solitaire", "1", "Complete Canfield by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Yukon", "Solitaire", "1", "Complete Yukon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Scorpion", "Solitaire", "1", "Complete Scorpion by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Forty Thieves", "Solitaire", "1", "Complete Forty Thieves by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Eight Off", "Solitaire", "1", "Complete Eight Off by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Baker's Game", "Solitaire", "1", "Complete Baker's Game by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Aces Up", "Solitaire", "1", "Complete Aces Up by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Calculation", "Solitaire", "1", "Complete Calculation by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Accordion", "Solitaire", "1", "Complete Accordion by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Cruel", "Solitaire", "1", "Complete Cruel by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Clock Solitaire", "Solitaire", "1", "Complete Clock Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("La Belle Lucie", "Solitaire", "1", "Complete La Belle Lucie by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Monte Carlo", "Solitaire", "1", "Complete Monte Carlo by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Penguin", "Solitaire", "1", "Complete Penguin by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Russian Solitaire", "Solitaire", "1", "Complete Russian Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Alaska", "Solitaire", "1", "Complete Alaska by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Westcliff", "Solitaire", "1", "Complete Westcliff by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Sir Tommy", "Solitaire", "1", "Complete Sir Tommy by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Seahaven Towers", "Solitaire", "1", "Complete Seahaven Towers by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Bisley", "Solitaire", "1", "Complete Bisley by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Klondike", "Solitaire", "1", "Complete Double Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Triple Klondike", "Solitaire", "1", "Complete Triple Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Beleaguered Castle", "Solitaire", "1", "Complete Beleaguered Castle by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Flower Garden", "Solitaire", "1", "Complete Flower Garden by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Streets and Alleys", "Solitaire", "1", "Complete Streets and Alleys by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Demon", "Solitaire", "1", "Complete Demon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Josephine", "Solitaire", "1", "Complete Josephine by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Limited", "Solitaire", "1", "Complete Limited by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Royal Cotillion", "Solitaire", "1", "Complete Royal Cotillion by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Nestor", "Solitaire", "1", "Complete Nestor by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Gaps", "Solitaire", "1", "Complete Gaps by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Montana", "Solitaire", "1", "Complete Montana by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Four Seasons", "Solitaire", "1", "Complete Four Seasons by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Windmill", "Solitaire", "1", "Complete Windmill by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Napoleon at St Helena", "Solitaire", "1", "Complete Napoleon at St Helena by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Simple Simon", "Solitaire", "1", "Complete Simple Simon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Rouge et Noir", "Solitaire", "1", "Complete Rouge et Noir by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Busy Aces", "Solitaire", "1", "Complete Busy Aces by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Good Measure", "Solitaire", "1", "Complete Good Measure by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Grandfather", "Solitaire", "1", "Complete Grandfather by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Grandmother", "Solitaire", "1", "Complete Grandmother by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Agnes", "Solitaire", "1", "Complete Agnes by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Canfield", "Solitaire", "1", "Complete Double Canfield by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Little Spider", "Solitaire", "1", "Complete Little Spider by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Spiderette", "Solitaire", "1", "Complete Spiderette by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Wasp", "Solitaire", "1", "Complete Wasp by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Will o' the Wisp", "Solitaire", "1", "Complete Will o' the Wisp by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Black Hole", "Solitaire", "1", "Complete Black Hole by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Eliminator", "Solitaire", "1", "Complete Eliminator by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Aunt Mary", "Solitaire", "1", "Complete Aunt Mary by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Alternations", "Solitaire", "1", "Complete Alternations by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Bristol", "Solitaire", "1", "Complete Bristol by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Capricieuse", "Solitaire", "1", "Complete Capricieuse by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Colorado", "Solitaire", "1", "Complete Colorado by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Congress", "Solitaire", "1", "Complete Congress by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Diplomat", "Solitaire", "1", "Complete Diplomat by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Easthaven", "Solitaire", "1", "Complete Easthaven by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Fortress", "Solitaire", "1", "Complete Fortress by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Fourteen Out", "Solitaire", "1", "Complete Fourteen Out by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Kings in the Corner Solitaire", "Solitaire", "1", "Complete Kings in the Corner Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Labyrinth", "Solitaire", "1", "Complete Labyrinth by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Lady Jane", "Solitaire", "1", "Complete Lady Jane by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Martha", "Solitaire", "1", "Complete Martha by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Miss Milligan", "Solitaire", "1", "Complete Miss Milligan by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Osmosis", "Solitaire", "1", "Complete Osmosis by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Peek", "Solitaire", "1", "Complete Peek by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Queen of Italy", "Solitaire", "1", "Complete Queen of Italy by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Rank and File", "Solitaire", "1", "Complete Rank and File by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Red and Black", "Solitaire", "1", "Complete Red and Black by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Royal Marriage", "Solitaire", "1", "Complete Royal Marriage by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Somerset", "Solitaire", "1", "Complete Somerset by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Spanish Patience", "Solitaire", "1", "Complete Spanish Patience by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Thieves of Egypt", "Solitaire", "1", "Complete Thieves of Egypt by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Trefoil", "Solitaire", "1", "Complete Trefoil by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Virginia Reel", "Solitaire", "1", "Complete Virginia Reel by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Whitehead", "Solitaire", "1", "Complete Whitehead by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Zodiac", "Solitaire", "1", "Complete Zodiac by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Acey and Kingsley", "Solitaire", "1", "Complete Acey and Kingsley by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Strategy", "Solitaire", "1", "Complete Strategy by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Yukon", "Solitaire", "1", "Complete Double Yukon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Australian Patience", "Solitaire", "1", "Complete Australian Patience by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Batsford", "Solitaire", "1", "Complete Batsford by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Carlton", "Solitaire", "1", "Complete Carlton by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Chinese Solitaire", "Solitaire", "1", "Complete Chinese Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Emperor", "Solitaire", "1", "Complete Emperor by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Fanny", "Solitaire", "1", "Complete Fanny by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Interchange", "Solitaire", "1", "Complete Interchange by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Jubilee", "Solitaire", "1", "Complete Jubilee by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("King Albert", "Solitaire", "1", "Complete King Albert by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Matrimony", "Solitaire", "1", "Complete Matrimony by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Napoleon's Square", "Solitaire", "1", "Complete Napoleon's Square by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Number Ten", "Solitaire", "1", "Complete Number Ten by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Parliament", "Solitaire", "1", "Complete Parliament by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Pas Seul", "Solitaire", "1", "Complete Pas Seul by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Perseverance", "Solitaire", "1", "Complete Perseverance by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Raglan", "Solitaire", "1", "Complete Raglan by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Salic Law", "Solitaire", "1", "Complete Salic Law by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("San Juan Hill", "Solitaire", "1", "Complete San Juan Hill by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Sly Fox", "Solitaire", "1", "Complete Sly Fox by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Tournament", "Solitaire", "1", "Complete Tournament by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Usk", "Solitaire", "1", "Complete Usk by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Vanishing Cross", "Solitaire", "1", "Complete Vanishing Cross by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Waterloo", "Solitaire", "1", "Complete Waterloo by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Zebra", "Solitaire", "1", "Complete Zebra by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Baker's Dozen", "Solitaire", "1", "Complete Baker's Dozen by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Castles in Spain", "Solitaire", "1", "Complete Castles in Spain by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Spanish Castle", "Solitaire", "1", "Complete Spanish Castle by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Good Thirteen", "Solitaire", "1", "Complete Good Thirteen by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Triple Peaks", "Solitaire", "1", "Complete Triple Peaks by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Addiction", "Solitaire", "1", "Complete Addiction by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Maze", "Solitaire", "1", "Complete Maze by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Sultan", "Solitaire", "1", "Complete Sultan by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Carpet", "Solitaire", "1", "Complete Carpet by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Terrace", "Solitaire", "1", "Complete Terrace by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Mississippi", "Solitaire", "1", "Complete Mississippi by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Octave", "Solitaire", "1", "Complete Octave by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Reserve", "Solitaire", "1", "Complete Reserve by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Storehouse", "Solitaire", "1", "Complete Storehouse by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Strategy Plus", "Solitaire", "1", "Complete Strategy Plus by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Virginia Reel Plus", "Solitaire", "1", "Complete Virginia Reel Plus by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Big Forty", "Solitaire", "1", "Complete Big Forty by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Crescent", "Solitaire", "1", "Complete Crescent by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Deuces", "Solitaire", "1", "Complete Deuces by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double FreeCell", "Solitaire", "1", "Complete Double FreeCell by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Scorpion", "Solitaire", "1", "Complete Double Scorpion by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Spider", "Solitaire", "1", "Complete Double Spider by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Pyramid", "Solitaire", "1", "Complete Double Pyramid by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double Golf", "Solitaire", "1", "Complete Double Golf by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Double TriPeaks", "Solitaire", "1", "Complete Double TriPeaks by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Triple Spider", "Solitaire", "1", "Complete Triple Spider by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Quadruple Spider", "Solitaire", "1", "Complete Quadruple Spider by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Relaxed FreeCell", "Solitaire", "1", "Complete Relaxed FreeCell by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Relaxed Klondike", "Solitaire", "1", "Complete Relaxed Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Vegas Klondike", "Solitaire", "1", "Complete Vegas Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Draw One Klondike", "Solitaire", "1", "Complete Draw One Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Draw Three Klondike", "Solitaire", "1", "Complete Draw Three Klondike by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Thoughtful Solitaire", "Solitaire", "1", "Complete Thoughtful Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Turncoat", "Solitaire", "1", "Complete Turncoat by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Blind Alleys", "Solitaire", "1", "Complete Blind Alleys by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("British Constitution", "Solitaire", "1", "Complete British Constitution by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Buffalo Bill", "Solitaire", "1", "Complete Buffalo Bill by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Carre Napoleon", "Solitaire", "1", "Complete Carre Napoleon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Citadel", "Solitaire", "1", "Complete Citadel by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Cribbage Solitaire", "Solitaire", "1", "Complete Cribbage Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Decade", "Solitaire", "1", "Complete Decade by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Duchess", "Solitaire", "1", "Complete Duchess by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Eagle Wing", "Solitaire", "1", "Complete Eagle Wing by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Eastcliff", "Solitaire", "1", "Complete Eastcliff by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Four Leaf Clovers", "Solitaire", "1", "Complete Four Leaf Clovers by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("German Patience", "Solitaire", "1", "Complete German Patience by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Glenwood", "Solitaire", "1", "Complete Glenwood by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Gold Mine", "Solitaire", "1", "Complete Gold Mine by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Grand Duchess", "Solitaire", "1", "Complete Grand Duchess by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Harp", "Solitaire", "1", "Complete Harp by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("House in the Wood", "Solitaire", "1", "Complete House in the Wood by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Junction", "Solitaire", "1", "Complete Junction by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Khedive", "Solitaire", "1", "Complete Khedive by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Kingdom", "Solitaire", "1", "Complete Kingdom by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("La Nivernaise", "Solitaire", "1", "Complete La Nivernaise by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Le Cadran", "Solitaire", "1", "Complete Le Cadran by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Lily", "Solitaire", "1", "Complete Lily by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Lucas", "Solitaire", "1", "Complete Lucas by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Milligan Cell", "Solitaire", "1", "Complete Milligan Cell by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Mount Olympus", "Solitaire", "1", "Complete Mount Olympus by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Mrs Mop", "Solitaire", "1", "Complete Mrs Mop by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Napoleon's Tomb", "Solitaire", "1", "Complete Napoleon's Tomb by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Pasha", "Solitaire", "1", "Complete Pasha by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Patriarchs", "Solitaire", "1", "Complete Patriarchs by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Raleigh", "Solitaire", "1", "Complete Raleigh by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Reserves", "Solitaire", "1", "Complete Reserves by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Rivers and Lakes", "Solitaire", "1", "Complete Rivers and Lakes by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Saratoga", "Solitaire", "1", "Complete Saratoga by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Shamrocks", "Solitaire", "1", "Complete Shamrocks by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Siegecraft", "Solitaire", "1", "Complete Siegecraft by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Somerset or Usk", "Solitaire", "1", "Complete Somerset or Usk by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Stalactites", "Solitaire", "1", "Complete Stalactites by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Super Flower Garden", "Solitaire", "1", "Complete Super Flower Garden by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Three Shuffles and a Draw", "Solitaire", "1", "Complete Three Shuffles and a Draw by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Thumb and Pouch", "Solitaire", "1", "Complete Thumb and Pouch by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Tower of Hanoy", "Solitaire", "1", "Complete Tower of Hanoy by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Westhaven", "Solitaire", "1", "Complete Westhaven by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Zeppelin", "Solitaire", "1", "Complete Zeppelin by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Agnes Bernauer", "Solitaire", "1", "Complete Agnes Bernauer by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Alternating Colors", "Solitaire", "1", "Complete Alternating Colors by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Archway", "Solitaire", "1", "Complete Archway by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Assembly", "Solitaire", "1", "Complete Assembly by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Auld Lang Syne", "Solitaire", "1", "Complete Auld Lang Syne by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Betsy Ross", "Solitaire", "1", "Complete Betsy Ross by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Big Ben", "Solitaire", "1", "Complete Big Ben by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Blockade", "Solitaire", "1", "Complete Blockade by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Boulevard", "Solitaire", "1", "Complete Boulevard by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Box Kite", "Solitaire", "1", "Complete Box Kite by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Busy Cards", "Solitaire", "1", "Complete Busy Cards by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Camelot", "Solitaire", "1", "Complete Camelot by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Captive Queens", "Solitaire", "1", "Complete Captive Queens by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Chameleon", "Solitaire", "1", "Complete Chameleon by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Chessboard", "Solitaire", "1", "Complete Chessboard by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Cicely", "Solitaire", "1", "Complete Cicely by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Clover Leaf", "Solitaire", "1", "Complete Clover Leaf by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Corner Card", "Solitaire", "1", "Complete Corner Card by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Courtyard", "Solitaire", "1", "Complete Courtyard by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Dover", "Solitaire", "1", "Complete Dover by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Duke", "Solitaire", "1", "Complete Duke by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Exiled Kings", "Solitaire", "1", "Complete Exiled Kings by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Fifteen Puzzle Solitaire", "Solitaire", "1", "Complete Fifteen Puzzle Solitaire by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Four Kingdoms", "Solitaire", "1", "Complete Four Kingdoms by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Gate", "Solitaire", "1", "Complete Gate by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Grandfather's Clock", "Solitaire", "1", "Complete Grandfather's Clock by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Great Wheel", "Solitaire", "1", "Complete Great Wheel by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Herringbone", "Solitaire", "1", "Complete Herringbone by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Intelligence", "Solitaire", "1", "Complete Intelligence by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Kingsley", "Solitaire", "1", "Complete Kingsley by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("La Chatelaine", "Solitaire", "1", "Complete La Chatelaine by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Light and Shade", "Solitaire", "1", "Complete Light and Shade by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Long Braid", "Solitaire", "1", "Complete Long Braid by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Louis", "Solitaire", "1", "Complete Louis by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Maria", "Solitaire", "1", "Complete Maria by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Midshipman", "Solitaire", "1", "Complete Midshipman by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Moojub", "Solitaire", "1", "Complete Moojub by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Open Crescent", "Solitaire", "1", "Complete Open Crescent by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Pairs", "Solitaire", "1", "Complete Pairs by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Puss in the Corner", "Solitaire", "1", "Complete Puss in the Corner by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Quadrille", "Solitaire", "1", "Complete Quadrille by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Rainbow", "Solitaire", "1", "Complete Rainbow by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Royal Parade", "Solitaire", "1", "Complete Royal Parade by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Salic Law Queen", "Solitaire", "1", "Complete Salic Law Queen by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Saxony", "Solitaire", "1", "Complete Saxony by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Seven Devils", "Solitaire", "1", "Complete Seven Devils by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Simplicity", "Solitaire", "1", "Complete Simplicity by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Six by Six", "Solitaire", "1", "Complete Six by Six by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("St Helena", "Solitaire", "1", "Complete St Helena by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Topsy Turvy Queens", "Solitaire", "1", "Complete Topsy Turvy Queens by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Twenty", "Solitaire", "1", "Complete Twenty by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Union Square", "Solitaire", "1", "Complete Union Square by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Wave Motion", "Solitaire", "1", "Complete Wave Motion by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Wildflower", "Solitaire", "1", "Complete Wildflower by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Windsor Castle", "Solitaire", "1", "Complete Windsor Castle by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Xerxes", "Solitaire", "1", "Complete Xerxes by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Yukon Cells", "Solitaire", "1", "Complete Yukon Cells by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Zigzag", "Solitaire", "1", "Complete Zigzag by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Big Bertha", "Solitaire", "1", "Complete Big Bertha by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Babette", "Solitaire", "1", "Complete Babette by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Casket", "Solitaire", "1", "Complete Casket by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Catalan", "Solitaire", "1", "Complete Catalan by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Corona", "Solitaire", "1", "Complete Corona by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Doublets", "Solitaire", "1", "Complete Doublets by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Frog", "Solitaire", "1", "Complete Frog by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Gargantua", "Solitaire", "1", "Complete Gargantua by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Giant", "Solitaire", "1", "Complete Giant by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Indian Patience", "Solitaire", "1", "Complete Indian Patience by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Jumbo", "Solitaire", "1", "Complete Jumbo by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("King's Audience", "Solitaire", "1", "Complete King's Audience by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("La Croix d'Honneur", "Solitaire", "1", "Complete La Croix d'Honneur by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Little Forty", "Solitaire", "1", "Complete Little Forty by organizing or clearing the layout before play stalls.", new[]
        {
            "Set up the tableau, stock, waste, foundations, and any reserves named by the variation.",
            "Move exposed cards according to the build rule, usually alternating color or matching suit, and fill empty spaces only as the variation allows.",
            "Use the stock or redeals only when no useful tableau move remains.",
            "Win by moving all cards to the foundations or by clearing the required layout.",
        }),
        new("Metropolitan", "Player vs Player", "2+", "Win Metropolitan by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Midnight Oil", "Player vs Player", "2+", "Win Midnight Oil by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mikado", "Player vs Player", "2+", "Win Mikado by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Napoleon's Flank", "Player vs Player", "2+", "Win Napoleon's Flank by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Odd and Even", "Player vs Player", "2+", "Win Odd and Even by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Patience", "Player vs Player", "2+", "Win Patience by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Quadrangle", "Player vs Player", "2+", "Win Quadrangle by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Rectangular", "Player vs Player", "2+", "Win Rectangular by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Royal East", "Player vs Player", "2+", "Win Royal East by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Seven Up Solitaire", "Player vs Player", "2+", "Win Seven Up Solitaire by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Square", "Player vs Player", "2+", "Win Square by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Step-Up", "Player vs Player", "2+", "Win Step-Up by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Twenty Four", "Player vs Player", "2+", "Win Twenty Four by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Usk and Somerset", "Player vs Player", "2+", "Win Usk and Somerset by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Victory", "Player vs Player", "2+", "Win Victory by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Wheatsheaf", "Player vs Player", "2+", "Win Wheatsheaf by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Block Eleven", "Player vs Player", "2+", "Win Block Eleven by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Bowling Solitaire", "Player vs Player", "2+", "Win Bowling Solitaire by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Elevens", "Player vs Player", "2+", "Win Elevens by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Fifteens", "Player vs Player", "2+", "Win Fifteens by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Hit or Miss", "Player vs Player", "2+", "Win Hit or Miss by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Joker Solitaire", "Player vs Player", "2+", "Win Joker Solitaire by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pairing", "Player vs Player", "2+", "Win Pairing by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Patient Pairs", "Player vs Player", "2+", "Win Patient Pairs by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Royal Rendezvous", "Player vs Player", "2+", "Win Royal Rendezvous by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Suit Elevens", "Player vs Player", "2+", "Win Suit Elevens by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Take Fourteen", "Player vs Player", "2+", "Win Take Fourteen by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Ten Across", "Player vs Player", "2+", "Win Ten Across by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Travellers", "Player vs Player", "2+", "Win Travellers by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Up and Down", "Player vs Player", "2+", "Win Up and Down by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Wish", "Player vs Player", "2+", "Win Wish by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Agram", "Player vs Player", "2+", "Win Agram by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("All Fives", "Player vs Player", "2+", "Win All Fives by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("All Fours", "Player vs Player", "2+", "Win All Fours by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Beggar-my-neighbor", "Player vs Player", "2+", "Win Beggar-my-neighbor by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Bezique", "Player vs Player", "2+", "Win Bezique by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Blackjack", "Player vs Player", "2+", "Win Blackjack by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Booray", "Player vs Player", "2+", "Win Booray by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Briscola", "Player vs Player", "2+", "Win Briscola by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Bridge", "Player vs Player", "2+", "Win Bridge by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("California Speed", "Player vs Player", "2+", "Win California Speed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Canasta", "Player vs Player", "2+", "Win Canasta by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Casino", "Player vs Player", "2+", "Win Casino by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cheat", "Player vs Player", "2+", "Win Cheat by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Crazy Eights", "Player vs Player", "2+", "Win Crazy Eights by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cribbage", "Player vs Player", "2+", "Win Cribbage by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Durak", "Player vs Player", "2+", "Win Durak by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Egyptian Ratscrew", "Player vs Player", "2+", "Win Egyptian Ratscrew by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Euchre", "Player vs Player", "2+", "Win Euchre by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Fan Tan", "Player vs Player", "2+", "Win Fan Tan by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Faro", "Player vs Player", "2+", "Win Faro by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Five Hundred", "Player vs Player", "2+", "Win Five Hundred by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Gin Rummy", "Player vs Player", "2+", "Win Gin Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Go Fish", "Player vs Player", "2+", "Win Go Fish by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Hearts", "Player vs Player", "2+", "Win Hearts by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Indian Rummy", "Player vs Player", "2+", "Win Indian Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Kemps", "Player vs Player", "2+", "Win Kemps by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Kings in the Corner", "Player vs Player", "2+", "Win Kings in the Corner by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mao", "Player vs Player", "2+", "Win Mao by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mau Mau", "Player vs Player", "2+", "Win Mau Mau by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Ninety-Nine", "Player vs Player", "2+", "Win Ninety-Nine by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Oh Hell", "Player vs Player", "2+", "Win Oh Hell by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Old Maid", "Player vs Player", "2+", "Win Old Maid by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Palace", "Player vs Player", "2+", "Win Palace by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pinochle", "Player vs Player", "2+", "Win Pinochle by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pitch", "Player vs Player", "2+", "Win Pitch by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Piquet", "Player vs Player", "2+", "Win Piquet by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("President", "Player vs Player", "2+", "Win President by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Rummy", "Player vs Player", "2+", "Win Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scat", "Player vs Player", "2+", "Win Scat by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sevens", "Player vs Player", "2+", "Win Sevens by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sheepshead", "Player vs Player", "2+", "Win Sheepshead by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Skat", "Player vs Player", "2+", "Win Skat by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Slapjack", "Player vs Player", "2+", "Win Slapjack by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spades", "Player vs Player", "2+", "Win Spades by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Speed", "Player vs Player", "2+", "Win Speed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spit", "Player vs Player", "2+", "Win Spit by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spoons", "Player vs Player", "2+", "Win Spoons by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tarot", "Player vs Player", "2+", "Win Tarot by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Thirty-One", "Player vs Player", "2+", "Win Thirty-One by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tonk", "Player vs Player", "2+", "Win Tonk by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tressette", "Player vs Player", "2+", "Win Tressette by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("War", "Player vs Player", "2+", "Win War by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Whist", "Player vs Player", "2+", "Win Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Yaniv", "Player vs Player", "2+", "Win Yaniv by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Zwickern", "Player vs Player", "2+", "Win Zwickern by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Belote", "Player vs Player", "2+", "Win Belote by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sueca", "Player vs Player", "2+", "Win Sueca by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Truco", "Player vs Player", "2+", "Win Truco by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Nap", "Player vs Player", "2+", "Win Nap by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Solo Whist", "Player vs Player", "2+", "Win Solo Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Contract Bridge", "Player vs Player", "2+", "Win Contract Bridge by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Duplicate Bridge", "Player vs Player", "2+", "Win Duplicate Bridge by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Rubber Bridge", "Player vs Player", "2+", "Win Rubber Bridge by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Honeymoon Bridge", "Player vs Player", "2+", "Win Honeymoon Bridge by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Bid Whist", "Player vs Player", "2+", "Win Bid Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Boston", "Player vs Player", "2+", "Win Boston by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cinch", "Player vs Player", "2+", "Win Cinch by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Clabber", "Player vs Player", "2+", "Win Clabber by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Conquian", "Player vs Player", "2+", "Win Conquian by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Continental Rummy", "Player vs Player", "2+", "Win Continental Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Kalooki", "Player vs Player", "2+", "Win Kalooki by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Manipulation Rummy", "Player vs Player", "2+", "Win Manipulation Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Oklahoma Gin", "Player vs Player", "2+", "Win Oklahoma Gin by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Panguingue", "Player vs Player", "2+", "Win Panguingue by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Samba", "Player vs Player", "2+", "Win Samba by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Shanghai Rummy", "Player vs Player", "2+", "Win Shanghai Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Skibo", "Player vs Player", "2+", "Win Skibo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Uno", "Player vs Player", "2+", "Win Uno by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Switch", "Player vs Player", "2+", "Win Switch by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Knockout Whist", "Player vs Player", "2+", "Win Knockout Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("German Whist", "Player vs Player", "2+", "Win German Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sergeant Major", "Player vs Player", "2+", "Win Sergeant Major by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Oh Pshaw", "Player vs Player", "2+", "Win Oh Pshaw by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Wizard", "Player vs Player", "2+", "Win Wizard by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Barbu", "Player vs Player", "2+", "Win Barbu by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Black Maria", "Player vs Player", "2+", "Win Black Maria by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cancellation Hearts", "Player vs Player", "2+", "Win Cancellation Hearts by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Omnibus Hearts", "Player vs Player", "2+", "Win Omnibus Hearts by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spot Hearts", "Player vs Player", "2+", "Win Spot Hearts by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Auction Pitch", "Player vs Player", "2+", "Win Auction Pitch by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("High-Low-Jack", "Player vs Player", "2+", "Win High-Low-Jack by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pedro", "Player vs Player", "2+", "Win Pedro by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Smear", "Player vs Player", "2+", "Win Smear by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Rook", "Player vs Player", "2+", "Win Rook by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tute", "Player vs Player", "2+", "Win Tute by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Escoba", "Player vs Player", "2+", "Win Escoba by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scopa", "Player vs Player", "2+", "Win Scopa by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cassino", "Player vs Player", "2+", "Win Cassino by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Brag", "Player vs Player", "2+", "Win Brag by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Teen Patti", "Player vs Player", "2+", "Win Teen Patti by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Three Card Poker", "Player vs Player", "2+", "Win Three Card Poker by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Texas Hold'em", "Player vs Player", "2+", "Win Texas Hold'em by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Omaha", "Player vs Player", "2+", "Win Omaha by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Seven Card Stud", "Player vs Player", "2+", "Win Seven Card Stud by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Five Card Draw", "Player vs Player", "2+", "Win Five Card Draw by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Lowball", "Player vs Player", "2+", "Win Lowball by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Badugi", "Player vs Player", "2+", "Win Badugi by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Baccarat", "Player vs Player", "2+", "Win Baccarat by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Red Dog", "Player vs Player", "2+", "Win Red Dog by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("In-Between", "Player vs Player", "2+", "Win In-Between by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Acey Deucey", "Player vs Player", "2+", "Win Acey Deucey by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cuckoo", "Player vs Player", "2+", "Win Cuckoo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Commerce", "Player vs Player", "2+", "Win Commerce by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pope Joan", "Player vs Player", "2+", "Win Pope Joan by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Newmarket", "Player vs Player", "2+", "Win Newmarket by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Michigan", "Player vs Player", "2+", "Win Michigan by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Lanterloo", "Player vs Player", "2+", "Win Lanterloo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Loo", "Player vs Player", "2+", "Win Loo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("�cart�", "Player vs Player", "2+", "Win �cart� by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Bezique Rubicon", "Player vs Player", "2+", "Win Bezique Rubicon by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pinochle Auction", "Player vs Player", "2+", "Win Pinochle Auction by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Double Deck Pinochle", "Player vs Player", "2+", "Win Double Deck Pinochle by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cutthroat Pinochle", "Player vs Player", "2+", "Win Cutthroat Pinochle by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Two-Handed Pinochle", "Player vs Player", "2+", "Win Two-Handed Pinochle by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sixty-Six", "Player vs Player", "2+", "Win Sixty-Six by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Schnapsen", "Player vs Player", "2+", "Win Schnapsen by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Marriage", "Player vs Player", "2+", "Win Marriage by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mari�", "Player vs Player", "2+", "Win Mari� by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Jass", "Player vs Player", "2+", "Win Jass by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Klaberjass", "Player vs Player", "2+", "Win Klaberjass by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Klaverjas", "Player vs Player", "2+", "Win Klaverjas by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Doppelkopf", "Player vs Player", "2+", "Win Doppelkopf by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Schafkopf", "Player vs Player", "2+", "Win Schafkopf by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Watten", "Player vs Player", "2+", "Win Watten by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Briscola Chiamata", "Player vs Player", "2+", "Win Briscola Chiamata by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tresette", "Player vs Player", "2+", "Win Tresette by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scopone", "Player vs Player", "2+", "Win Scopone by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mus", "Player vs Player", "2+", "Win Mus by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Buraco", "Player vs Player", "2+", "Win Buraco by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Carioca", "Player vs Player", "2+", "Win Carioca by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scala Quaranta", "Player vs Player", "2+", "Win Scala Quaranta by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Phase 10", "Player vs Player", "2+", "Win Phase 10 by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Skip-Bo", "Player vs Player", "2+", "Win Skip-Bo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Dutch Blitz", "Player vs Player", "2+", "Win Dutch Blitz by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Ligretto", "Player vs Player", "2+", "Win Ligretto by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Setback", "Player vs Player", "2+", "Win Setback by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Hokm", "Player vs Player", "2+", "Win Hokm by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Court Piece", "Player vs Player", "2+", "Win Court Piece by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Rang", "Player vs Player", "2+", "Win Rang by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tarneeb", "Player vs Player", "2+", "Win Tarneeb by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Trix", "Player vs Player", "2+", "Win Trix by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Baloot", "Player vs Player", "2+", "Win Baloot by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Preferans", "Player vs Player", "2+", "Win Preferans by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Durak Podkidnoy", "Player vs Player", "2+", "Win Durak Podkidnoy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Durak Perevodnoy", "Player vs Player", "2+", "Win Durak Perevodnoy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Sedma", "Player vs Player", "2+", "Win Sedma by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Mendikot", "Player vs Player", "2+", "Win Mendikot by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Danish Whist", "Player vs Player", "2+", "Win Danish Whist by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Oh Hell Wizard", "Player vs Player", "2+", "Win Oh Hell Wizard by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Dummy Rummy", "Player vs Player", "2+", "Win Dummy Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Liverpool Rummy", "Player vs Player", "2+", "Win Liverpool Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Contract Rummy", "Player vs Player", "2+", "Win Contract Rummy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("May I", "Player vs Player", "2+", "Win May I by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Hand and Foot", "Player vs Player", "2+", "Win Hand and Foot by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Continental", "Player vs Player", "2+", "Win Continental by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Burraco", "Player vs Player", "2+", "Win Burraco by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Canasta Bolivia", "Player vs Player", "2+", "Win Canasta Bolivia by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Canasta Caliente", "Player vs Player", "2+", "Win Canasta Caliente by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Samba Canasta", "Player vs Player", "2+", "Win Samba Canasta by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spite and Malice", "Player vs Player", "2+", "Win Spite and Malice by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Kings Corners", "Player vs Player", "2+", "Win Kings Corners by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Golf Card Game", "Player vs Player", "2+", "Win Golf Card Game by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cambio", "Player vs Player", "2+", "Win Cambio by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cabo", "Player vs Player", "2+", "Win Cabo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Haggis", "Player vs Player", "2+", "Win Haggis by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("The Great Dalmuti", "Player vs Player", "2+", "Win The Great Dalmuti by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tichu", "Player vs Player", "2+", "Win Tichu by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Big Two", "Player vs Player", "2+", "Win Big Two by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Dou Dizhu", "Player vs Player", "2+", "Win Dou Dizhu by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tien Len", "Player vs Player", "2+", "Win Tien Len by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("President Asshole", "Player vs Player", "2+", "Win President Asshole by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Daifugo", "Player vs Player", "2+", "Win Daifugo by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Zheng Fen", "Player vs Player", "2+", "Win Zheng Fen by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Fight the Landlord", "Player vs Player", "2+", "Win Fight the Landlord by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pusoy Dos", "Player vs Player", "2+", "Win Pusoy Dos by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tong-its", "Player vs Player", "2+", "Win Tong-its by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pusoy", "Player vs Player", "2+", "Win Pusoy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Chinch�n", "Player vs Player", "2+", "Win Chinch�n by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Escoba de 15", "Player vs Player", "2+", "Win Escoba de 15 by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pocha", "Player vs Player", "2+", "Win Pocha by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Podrida", "Player vs Player", "2+", "Win Podrida by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Brisca", "Player vs Player", "2+", "Win Brisca by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Aluette", "Player vs Player", "2+", "Win Aluette by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Belote Coinch�e", "Player vs Player", "2+", "Win Belote Coinch�e by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Coinche", "Player vs Player", "2+", "Win Coinche by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Tarabish", "Player vs Player", "2+", "Win Tarabish by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cribbage Five Card", "Player vs Player", "2+", "Win Cribbage Five Card by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cribbage Six Card", "Player vs Player", "2+", "Win Cribbage Six Card by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cribbage Lowball", "Player vs Player", "2+", "Win Cribbage Lowball by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cribbage Noddy", "Player vs Player", "2+", "Win Cribbage Noddy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cassino Royal", "Player vs Player", "2+", "Win Cassino Royal by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cassino Spade", "Player vs Player", "2+", "Win Cassino Spade by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Casino Draw", "Player vs Player", "2+", "Win Casino Draw by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Royal Casino", "Player vs Player", "2+", "Win Royal Casino by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Klondike by Threes", "Player vs Player", "2+", "Win Klondike by Threes by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("FreeCell Baker", "Player vs Player", "2+", "Win FreeCell Baker by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spider One Suit", "Player vs Player", "2+", "Win Spider One Suit by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spider Two Suits", "Player vs Player", "2+", "Win Spider Two Suits by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Spider Four Suits", "Player vs Player", "2+", "Win Spider Four Suits by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pyramid Relaxed", "Player vs Player", "2+", "Win Pyramid Relaxed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Pyramid Strict", "Player vs Player", "2+", "Win Pyramid Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("TriPeaks Open", "Player vs Player", "2+", "Win TriPeaks Open by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("TriPeaks Closed", "Player vs Player", "2+", "Win TriPeaks Closed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Golf Relaxed", "Player vs Player", "2+", "Win Golf Relaxed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Golf Strict", "Player vs Player", "2+", "Win Golf Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Yukon Russian", "Player vs Player", "2+", "Win Yukon Russian by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Yukon Alaska", "Player vs Player", "2+", "Win Yukon Alaska by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Forty Thieves Open", "Player vs Player", "2+", "Win Forty Thieves Open by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Forty Thieves Strict", "Player vs Player", "2+", "Win Forty Thieves Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Canfield Storehouse", "Player vs Player", "2+", "Win Canfield Storehouse by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Canfield Rainbow", "Player vs Player", "2+", "Win Canfield Rainbow by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scorpion Easy", "Player vs Player", "2+", "Win Scorpion Easy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Scorpion Hard", "Player vs Player", "2+", "Win Scorpion Hard by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Accordion Easy", "Player vs Player", "2+", "Win Accordion Easy by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Accordion Strict", "Player vs Player", "2+", "Win Accordion Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Monte Carlo Thirteens", "Player vs Player", "2+", "Win Monte Carlo Thirteens by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Monte Carlo Same Rank", "Player vs Player", "2+", "Win Monte Carlo Same Rank by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Clock Patience", "Player vs Player", "2+", "Win Clock Patience by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Clock Open", "Player vs Player", "2+", "Win Clock Open by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Aces Up Relaxed", "Player vs Player", "2+", "Win Aces Up Relaxed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Aces Up Strict", "Player vs Player", "2+", "Win Aces Up Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Calculation Open", "Player vs Player", "2+", "Win Calculation Open by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Calculation Strict", "Player vs Player", "2+", "Win Calculation Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("La Belle Lucie Fan", "Player vs Player", "2+", "Win La Belle Lucie Fan by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("La Belle Lucie Three Shuffles", "Player vs Player", "2+", "Win La Belle Lucie Three Shuffles by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cruel Redeal", "Player vs Player", "2+", "Win Cruel Redeal by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Cruel No Redeal", "Player vs Player", "2+", "Win Cruel No Redeal by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Penguin Open", "Player vs Player", "2+", "Win Penguin Open by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Penguin Cells", "Player vs Player", "2+", "Win Penguin Cells by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Eight Off Relaxed", "Player vs Player", "2+", "Win Eight Off Relaxed by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
        new("Eight Off Strict", "Player vs Player", "2+", "Win Eight Off Strict by outscoring, outplaying, or outlasting the other players.", new[]
        {
            "Choose dealer or first player, shuffle, and deal the listed hand size or the game standard.",
            "On your turn draw, bid, lead, follow suit, meld, capture, discard, or play a card according to the family of the game.",
            "Score tricks, captures, combinations, card points, or penalties at the end of each hand.",
            "Keep playing hands until a player or team reaches the target score, empties their hand, or wins the final showdown.",
        }),
    };
}

public sealed record CardGameRuleSet(string Name, string Category, string Players, string Objective, IReadOnlyList<string> HowToPlay);

public sealed record CardGameStyleGroup(string Name, string Description, IReadOnlyList<string> GameNames);
