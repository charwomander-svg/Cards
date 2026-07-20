using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class SheddingRules : ICardGameRules
{
    private readonly int _cardsPerPlayer;

    public SheddingRules(string name, int minPlayers = 2, int maxPlayers = 8, int cardsPerPlayer = 7)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (minPlayers < 2)
            throw new ArgumentOutOfRangeException(nameof(minPlayers), "Shedding games need at least two players.");
        if (maxPlayers < minPlayers)
            throw new ArgumentOutOfRangeException(nameof(maxPlayers), "Maximum players cannot be less than minimum players.");
        if (cardsPerPlayer < 1)
            throw new ArgumentOutOfRangeException(nameof(cardsPerPlayer), "Cards per player must be positive.");

        Name = name;
        MinPlayers = minPlayers;
        MaxPlayers = maxPlayers;
        _cardsPerPlayer = cardsPerPlayer;
    }

    public string Name { get; }
    public int MinPlayers { get; }
    public int MaxPlayers { get; }
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, true, true, false, false, false, false, false, true, false, new[] { "play", "draw" });

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
        Card top = session.GetPile("discard").Top;
        var moves = session.Hands[session.CurrentPlayerIndex].Cards
            .Where(card => CanPlay(card, top))
            .Select(card => new MoveDescriptor("play", card.ToString(), "discard", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() }))
            .ToList();

        if (!session.GetPile("stock").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", null, PlayerIndex: session.CurrentPlayerIndex));

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        int playerIndex = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (playerIndex != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
        {
            CardPile stock = session.GetPile("stock");
            if (stock.IsEmpty)
                return MoveResult.Failure("The stock is empty.");

            session.Hands[playerIndex].AddCard(stock.Draw());
            session.RecordDraw(playerIndex, "stock", 1);
            session.AdvanceTurn();
            return MoveResult.Success("Drew one card.");
        }

        if (!move.Type.Equals("play", StringComparison.OrdinalIgnoreCase))
            return MoveResult.Failure($"Unsupported move '{move.Type}'.");

        Hand hand = session.Hands[playerIndex];
        Card? card = hand.Cards.FirstOrDefault(c => c.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
        if (card is null)
            return MoveResult.Failure("The selected card is not in the current player's hand.");

        CardPile discard = session.GetPile("discard");
        if (!CanPlay(card, discard.Top))
            return MoveResult.Failure("Card must match rank or suit of the discard top.");

        hand.PlayCard(card);
        discard.Add(card);
        session.RecordPlay(playerIndex, "discard", 1);
        session.IsComplete = hand.IsEmpty;
        if (session.IsComplete)
            session.DetermineWinnerByEmptyHand();
        session.AdvanceTurn();
        return MoveResult.Success($"{card} played.");
    }

    private static bool CanPlay(Card card, Card top) => card.Rank == top.Rank || card.Suit == top.Suit;
}
