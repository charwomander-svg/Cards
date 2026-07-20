using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class RummyRules : ICardGameRules
{
    private readonly int _cardsPerPlayer;

    public RummyRules(string name = "Rummy", int minPlayers = 2, int maxPlayers = 6, int cardsPerPlayer = 7)
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
        MinPlayers = minPlayers;
        MaxPlayers = maxPlayers;
        _cardsPerPlayer = cardsPerPlayer;
    }

    public string Name { get; }
    public int MinPlayers { get; }
    public int MaxPlayers { get; }
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, MaxPlayers > 2, true, true, false, false, true, false, false, true, true, new[] { "draw", "discard", "meld", "knock" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} supports {MinPlayers}-{MaxPlayers} players.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);
        session.DealToEach(deck, _cardsPerPlayer);
        session.AddPile("stock").AddRange(deck.Cards);
        session.AddPile("discard").Add(session.GetPile("stock").Draw());
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        int player = session.CurrentPlayerIndex;
        var moves = new List<MoveDescriptor>();
        if (!session.GetPile("stock").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", PlayerIndex: player));
        if (!session.GetPile("discard").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "discard", PlayerIndex: player));

        moves.AddRange(session.Hands[player].Cards.Select(card => new MoveDescriptor("discard", card.ToString(), "discard", PlayerIndex: player)));
        moves.AddRange(FindMelds(session.Hands[player].Cards).Select(meld => new MoveDescriptor("meld", "hand", PlayerIndex: player, Cards: meld.Select(card => card.ToString()).ToArray())));
        if (CalculateDeadwood(session.Hands[player].Cards) <= 10)
            moves.Add(new MoveDescriptor("knock", "hand", PlayerIndex: player));

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);
        int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (player != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        Hand hand = session.Hands[player];
        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
        {
            CardPile source = session.GetPile(move.Source);
            if (source.IsEmpty)
                return MoveResult.Failure("Draw source is empty.");
            hand.AddCard(source.Draw());
            session.RecordDraw(player, source.Name, 1);
            return MoveResult.Success("Card drawn.");
        }

        if (move.Type.Equals("discard", StringComparison.OrdinalIgnoreCase))
        {
            Card? card = FindCard(hand, move.Source);
            if (card is null)
                return MoveResult.Failure("The selected card is not in the player's hand.");
            hand.PlayCard(card);
            session.GetPile("discard").Add(card);
            session.RecordDiscard(player, "discard", 1);
            int deadwood = CalculateDeadwood(hand.Cards);
            session.Scores[player] = session.Scores.GetValueOrDefault(player) - deadwood;
            session.RecordScore(player, Math.Max(1, 100 - deadwood));
            session.AdvanceTurn();
            return MoveResult.Success($"{card} discarded.");
        }

        if (move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase))
        {
            Card[] cards = ResolveCards(hand, move.Cards);
            if (!IsValidMeld(cards))
                return MoveResult.Failure("Rummy meld must be a same-rank set or same-suit run of at least three cards.");

            int score = cards.Sum(CardValue);
            session.RecordScore(player, score);
            return session.ApplyGenericAction(move);
        }

        if (move.Type.Equals("knock", StringComparison.OrdinalIgnoreCase))
        {
            int deadwood = CalculateDeadwood(hand.Cards);
            session.Scores[player] = session.Scores.GetValueOrDefault(player) - deadwood;
            session.RecordScore(player, -deadwood);
            session.IsComplete = true;
            session.DetermineWinnerByHighScore();
            return MoveResult.Success($"Player knocked with {deadwood} deadwood.");
        }

        return MoveResult.Failure($"Unsupported Rummy move '{move.Type}'.");
    }

    public static bool IsValidMeld(IReadOnlyList<Card> cards)
    {
        if (cards.Count < 3)
            return false;

        bool sameRank = cards.All(card => card.Rank == cards[0].Rank);
        bool sameSuitRun = cards.All(card => card.Suit == cards[0].Suit)
            && cards.Select(card => (int)card.Rank).Order().SequenceEqual(Enumerable.Range(cards.Min(card => (int)card.Rank), cards.Count));
        return sameRank || sameSuitRun;
    }

    public static int CalculateDeadwood(IReadOnlyList<Card> hand)
    {
        var meldCards = new HashSet<Card>(FindMelds(hand).OrderByDescending(meld => meld.Count).FirstOrDefault() ?? Array.Empty<Card>());
        return hand.Where(card => !meldCards.Contains(card)).Sum(CardValue);
    }

    private static IReadOnlyList<IReadOnlyList<Card>> FindMelds(IReadOnlyList<Card> cards)
    {
        var melds = new List<IReadOnlyList<Card>>();
        melds.AddRange(cards.GroupBy(card => card.Rank).Where(group => group.Count() >= 3).Select(group => (IReadOnlyList<Card>)group.ToArray()));
        melds.AddRange(cards.GroupBy(card => card.Suit).SelectMany(group =>
            group.OrderBy(card => (int)card.Rank)
                .Select((card, index) => new { card, index })
                .GroupBy(item => (int)item.card.Rank - item.index)
                .Where(run => run.Count() >= 3)
                .Select(run => (IReadOnlyList<Card>)run.Select(item => item.card).ToArray())));
        return melds;
    }

    private static int CardValue(Card card) => card.Rank is Rank.Jack or Rank.Queen or Rank.King ? 10 : (int)card.Rank;
    public static Card? FindCard(Hand hand, string cardName) => hand.Cards.FirstOrDefault(card => card.ToString().Equals(cardName, StringComparison.OrdinalIgnoreCase));
    public static Card[] ResolveCards(Hand hand, IReadOnlyList<string>? cardNames) =>
        cardNames is null ? Array.Empty<Card>() : cardNames.Select(name => FindCard(hand, name)).OfType<Card>().ToArray();
}
