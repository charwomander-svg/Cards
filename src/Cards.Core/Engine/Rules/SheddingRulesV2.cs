using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class SheddingRulesV2 : ICardGameRules
{
    private readonly int _cardsPerPlayer;
    private readonly Dictionary<CardGameSession, SheddingGameState> _states = new();

    public SheddingRulesV2(string name, int minPlayers = 2, int maxPlayers = 8, int cardsPerPlayer = 7)
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
        MinPlayers = minPlayers;
        MaxPlayers = maxPlayers;
        _cardsPerPlayer = cardsPerPlayer;
    }

    public string Name { get; }
    public int MinPlayers { get; }
    public int MaxPlayers { get; }
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, true, true, false, false, false, false, false, true, false, new[] { "play", "draw" });

    public SheddingGameState GetState(CardGameSession session)
    {
        if (!_states.TryGetValue(session, out SheddingGameState? state))
        {
            state = new SheddingGameState();
            _states[session] = state;
        }

        return state;
    }

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
        _states[session] = new SheddingGameState();
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        SheddingGameState state = GetState(session);
        Card top = session.GetPile("discard").Top;
        var moves = session.Hands[session.CurrentPlayerIndex].Cards
            .Where(card => CanPlay(card, top, state.ActiveSuit))
            .Select(card => new MoveDescriptor("play", card.ToString(), "discard", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() }))
            .ToList();

        if (!session.GetPile("stock").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", PlayerIndex: session.CurrentPlayerIndex, Count: Math.Max(1, state.PendingDrawCount)));

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);
        SheddingGameState state = GetState(session);
        int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (player != session.CurrentPlayerIndex && !(move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase) && state.PendingDrawCount > 0))
            return MoveResult.Failure("It is not that player's turn.");

        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
            return Draw(session, state, player);

        if (!move.Type.Equals("play", StringComparison.OrdinalIgnoreCase))
            return MoveResult.Failure($"Unsupported move '{move.Type}'.");

        Hand hand = session.Hands[player];
        Card? card = hand.Cards.FirstOrDefault(c => c.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
        if (card is null)
            return MoveResult.Failure("The selected card is not in the player's hand.");
        CardPile discard = session.GetPile("discard");
        if (!CanPlay(card, discard.Top, state.ActiveSuit))
            return MoveResult.Failure("Card must match rank, suit, active suit, or be an eight.");

        hand.PlayCard(card);
        discard.Add(card);
        session.RecordPlay(player, "discard", 1);
        ApplySpecialEffect(session, state, card, move.DeclaredSuit);
        session.IsComplete = hand.IsEmpty;
        if (session.IsComplete)
            session.DetermineWinnerByEmptyHand();
        else if (card.Rank is not Rank.Queen and not Rank.Ace)
            AdvanceByDirection(session, state, 1);
        return MoveResult.Success($"{card} played.");
    }

    private static MoveResult Draw(CardGameSession session, SheddingGameState state, int player)
    {
        CardPile stock = session.GetPile("stock");
        int count = Math.Max(1, state.ConsumeDrawPenalty());
        if (stock.Count < count)
            count = stock.Count;
        if (count == 0)
            return MoveResult.Failure("The stock is empty.");

        for (int i = 0; i < count; i++)
            session.Hands[player].AddCard(stock.Draw());
        session.RecordDraw(player, "stock", count);
        AdvanceByDirection(session, state, 1);
        return MoveResult.Success($"Drew {count} card(s).");
    }

    private static void ApplySpecialEffect(CardGameSession session, SheddingGameState state, Card card, Suit? declaredSuit)
    {
        state.SetActiveSuit(card.Rank == Rank.Eight ? declaredSuit ?? card.Suit : card.Suit);
        if (card.Rank == Rank.Two)
            state.AddDrawPenalty(2);
        if (card.Rank == Rank.Ace)
            state.Reverse();
        if (card.Rank == Rank.Queen)
            AdvanceByDirection(session, state, 2);
    }

    private static bool CanPlay(Card card, Card top, Suit? activeSuit) =>
        card.Rank == Rank.Eight || card.Rank == top.Rank || card.Suit == top.Suit || (activeSuit is not null && card.Suit == activeSuit);

    private static void AdvanceByDirection(CardGameSession session, SheddingGameState state, int steps)
    {
        int next = session.CurrentPlayerIndex;
        for (int i = 0; i < steps; i++)
            next = (next + state.Direction + session.PlayerCount) % session.PlayerCount;
        session.CurrentPlayerIndex = next;
        session.Statistics.RecordTurn((next - state.Direction + session.PlayerCount) % session.PlayerCount);
    }
}
