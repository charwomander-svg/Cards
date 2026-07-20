using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class TrickTakingRules : ICardGameRules
{
    private readonly int _cardsPerPlayer;

    public TrickTakingRules(string name, int minPlayers = 2, int maxPlayers = 4, int cardsPerPlayer = 5)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (minPlayers < 2)
            throw new ArgumentOutOfRangeException(nameof(minPlayers), "Trick-taking games need at least two players.");
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
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, MaxPlayers == 4, true, true, false, true, Name.Contains("Pinochle", StringComparison.OrdinalIgnoreCase) || Name.Contains("Canasta", StringComparison.OrdinalIgnoreCase), false, true, false, false, new[] { "play" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} supports {MinPlayers}-{MaxPlayers} players.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);
        session.DealToEach(deck, _cardsPerPlayer);
        session.AddPile("trick");
        session.AddPile("discard");
        session.AddPile("stock").AddRange(deck.Cards);
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        Hand hand = session.Hands[session.CurrentPlayerIndex];
        if (hand.IsEmpty)
            return Array.Empty<MoveDescriptor>();

        return hand.Cards
                    .Select(card => new MoveDescriptor("play", card.ToString(), "trick", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() }))
            .ToArray();
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        if (!move.Type.Equals("play", StringComparison.OrdinalIgnoreCase))
            return MoveResult.Failure($"Unsupported move '{move.Type}'.");

        int playerIndex = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (playerIndex != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        Hand hand = session.Hands[playerIndex];
        Card? card = hand.Cards.FirstOrDefault(c => c.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
        if (card is null)
            return MoveResult.Failure("The selected card is not in the current player's hand.");

        hand.PlayCard(card);
        session.GetPile("trick").Add(card);
        session.RecordPlay(playerIndex, "trick", 1);
        session.Scores[playerIndex] = session.Scores.GetValueOrDefault(playerIndex) + (int)card.Rank;
        session.RecordScore(playerIndex, (int)card.Rank);
        session.IsComplete = session.Hands.All(h => h.IsEmpty);
        if (session.IsComplete)
            session.DetermineWinnerByHighScore();
        session.AdvanceTurn();
        return MoveResult.Success($"{card} played.");
    }
}
