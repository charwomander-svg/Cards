using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public abstract class TrickTakingRulesBase : ICardGameRules
{
    private readonly Dictionary<CardGameSession, TrickTakingState> _states = new();

    protected TrickTakingRulesBase(string name, int minPlayers, int maxPlayers, int cardsPerPlayer)
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Game name is required.", nameof(name)) : name;
        MinPlayers = minPlayers;
        MaxPlayers = maxPlayers;
        CardsPerPlayer = cardsPerPlayer;
    }

    public string Name { get; }
    public int MinPlayers { get; }
    public int MaxPlayers { get; }
    public int CardsPerPlayer { get; }
    public virtual RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, MaxPlayers == 4, true, true, false, true, false, false, true, false, false, new[] { "bid", "play" });

    public TrickTakingState GetState(CardGameSession session)
    {
        if (!_states.TryGetValue(session, out TrickTakingState? state))
        {
            state = new TrickTakingState();
            _states[session] = state;
        }

        return state;
    }

    public virtual CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} supports {MinPlayers}-{MaxPlayers} players.");

        var session = new CardGameSession(Name, playerCount);
        Deal(session, random);
        session.AddPile("trick");
        session.AddPile("won");
        if (playerCount == 4)
            session.AssignAlternatingTeams(2);
        _states[session] = new TrickTakingState();
        return session;
    }

    public virtual IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        Hand hand = session.Hands[session.CurrentPlayerIndex];
        if (hand.IsEmpty)
            return Array.Empty<MoveDescriptor>();

        TrickTakingState state = GetState(session);
        Card[] legalCards = GetLegalCards(hand, state.LedSuit).ToArray();
        return legalCards.Select(card => new MoveDescriptor("play", card.ToString(), "trick", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() })).ToArray();
    }

    public virtual MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        if (move.Type.Equals("bid", StringComparison.OrdinalIgnoreCase))
        {
            MoveResult result = session.ApplyGenericAction(move);
            if (result.Succeeded)
            {
                TrickTakingState bidState = GetState(session);
                bidState.DeclarerIndex = move.PlayerIndex;
                bidState.ContractLevel = move.Bid;
            }
            return result;
        }

        if (!move.Type.Equals("play", StringComparison.OrdinalIgnoreCase))
            return MoveResult.Failure($"Unsupported trick-taking move '{move.Type}'.");

        int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (player != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        Hand hand = session.Hands[player];
        Card? card = hand.Cards.FirstOrDefault(c => c.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
        if (card is null)
            return MoveResult.Failure("The selected card is not in the player's hand.");

        TrickTakingState state = GetState(session);
        if (!GetLegalCards(hand, state.LedSuit).Contains(card))
            return MoveResult.Failure("Player must follow suit when able.");

        hand.PlayCard(card);
        state.AddPlay(player, card);
        session.GetPile("trick").Add(card);
        session.RecordPlay(player, "trick", 1);

        if (state.CurrentTrick.Count == session.PlayerCount)
            CompleteTrick(session, state);
        else
            session.AdvanceTurn();

        session.IsComplete = session.Hands.All(h => h.IsEmpty) && state.CurrentTrick.Count == 0;
        if (session.IsComplete)
            session.DetermineWinnerByHighScore();
        return MoveResult.Success($"{card} played.");
    }

    protected virtual IEnumerable<Card> GetLegalCards(Hand hand, Suit? ledSuit)
    {
        if (ledSuit is null)
            return hand.Cards;

        Card[] matchingSuit = hand.Cards.Where(card => card.Suit == ledSuit).ToArray();
        return matchingSuit.Length == 0 ? hand.Cards : matchingSuit;
    }

    protected virtual int CompareCards(Card candidate, Card incumbent, Suit ledSuit, Suit? trumpSuit)
    {
        bool candidateTrump = trumpSuit is not null && candidate.Suit == trumpSuit;
        bool incumbentTrump = trumpSuit is not null && incumbent.Suit == trumpSuit;
        if (candidateTrump != incumbentTrump)
            return candidateTrump ? 1 : -1;
        if (candidate.Suit == incumbent.Suit)
            return RankStrength(candidate).CompareTo(RankStrength(incumbent));
        if (candidate.Suit == ledSuit && incumbent.Suit != ledSuit)
            return 1;
        return -1;
    }

    protected virtual int RankStrength(Card card) => (int)card.Rank;
    protected abstract void Deal(CardGameSession session, Random? random);
    protected virtual int TrickScore(IReadOnlyList<TrickPlay> trick) => trick.Sum(play => (int)play.Card.Rank);

    private void CompleteTrick(CardGameSession session, TrickTakingState state)
    {
        Suit ledSuit = state.LedSuit ?? state.CurrentTrick[0].Card.Suit;
        TrickPlay winner = state.CurrentTrick[0];
        foreach (TrickPlay play in state.CurrentTrick.Skip(1))
            if (CompareCards(play.Card, winner.Card, ledSuit, state.TrumpSuit) > 0)
                winner = play;

        int score = TrickScore(state.CurrentTrick);
        session.Scores[winner.PlayerIndex] = session.Scores.GetValueOrDefault(winner.PlayerIndex) + score;
        session.RecordScore(winner.PlayerIndex, score);
        session.GetPile("trick").Clear();
        state.ClearTrick();
        session.CurrentPlayerIndex = winner.PlayerIndex;
    }
}
