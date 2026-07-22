using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class DrawDiscardRules : ICardGameRules
{
    private readonly int _cardsPerPlayer;

    public DrawDiscardRules(string name, int minPlayers = 2, int maxPlayers = 6, int cardsPerPlayer = 7)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (minPlayers < 2)
            throw new ArgumentOutOfRangeException(nameof(minPlayers), "Draw/discard games need at least two players.");
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
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, MaxPlayers > 2, true, true, false, false, true, false, false, true, true, new[] { "draw", "discard" });

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
        var moves = new List<MoveDescriptor>();

        if (!session.GetPile("stock").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "stock", null, PlayerIndex: session.CurrentPlayerIndex));
        if (!session.GetPile("discard").IsEmpty)
            moves.Add(new MoveDescriptor("draw", "discard", null, PlayerIndex: session.CurrentPlayerIndex));

        moves.AddRange(session.Hands[session.CurrentPlayerIndex].Cards.Select(card =>
                    new MoveDescriptor("discard", card.ToString(), "discard", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() })));

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        int playerIndex = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (playerIndex != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        Hand hand = session.Hands[playerIndex];
        if (move.Type.Equals("draw", StringComparison.OrdinalIgnoreCase))
        {
            CardPile source = session.GetPile(move.Source);
            if (source.IsEmpty)
                return MoveResult.Failure("Draw source is empty.");

            hand.AddCard(source.Draw());
            session.RecordDraw(playerIndex, source.Name, 1);
            return MoveResult.Success("Card drawn.");
        }

        if (move.Type.Equals("discard", StringComparison.OrdinalIgnoreCase))
        {
            Card? card = hand.Cards.FirstOrDefault(c => c.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
            if (card is null)
                return MoveResult.Failure("The selected card is not in the current player's hand.");

            hand.PlayCard(card);
            session.GetPile("discard").Add(card);
            session.RecordDiscard(playerIndex, "discard", 1);
            session.Scores[playerIndex] = session.Scores.GetValueOrDefault(playerIndex) + ScoreHand(hand);
            session.RecordScore(playerIndex, ScoreHand(hand));
            session.IsComplete = hand.IsEmpty;
            if (session.IsComplete)
                session.DetermineWinnerByEmptyHand();
            session.AdvanceTurn();
            return MoveResult.Success($"{card} discarded.");
        }

        return MoveResult.Failure($"Unsupported move '{move.Type}'.");
    }

    private static int ScoreHand(Hand hand) => hand.Cards.Sum(card => (int)card.Rank);
}
