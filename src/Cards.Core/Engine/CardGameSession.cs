using Cards.Core.Models;
using Cards.Core.Engine.Statistics;

namespace Cards.Core.Engine;

/// <summary>Shared runtime state for a rules-driven card game.</summary>
public sealed class CardGameSession
{
    private readonly Dictionary<string, CardPile> _piles = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<GameAction> _history = new();
    private readonly Dictionary<int, int> _playerTeams = new();
    private readonly Dictionary<int, int> _bids = new();
    private readonly Dictionary<int, List<IReadOnlyList<Card>>> _melds = new();

    public CardGameSession(string gameName, int playerCount)
    {
        if (string.IsNullOrWhiteSpace(gameName))
            throw new ArgumentException("Game name is required.", nameof(gameName));
        if (playerCount < 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Must have at least one player.");

        GameName = gameName;
        PlayerCount = playerCount;
        Hands = Enumerable.Range(0, playerCount).Select(_ => new Hand()).ToArray();
    }

    public string GameName { get; }
    public int PlayerCount { get; }
    public int CurrentPlayerIndex { get; set; }
    public int Round { get; set; } = 1;
    public IReadOnlyList<Hand> Hands { get; }
    public IReadOnlyDictionary<string, CardPile> Piles => _piles;
    public Dictionary<int, int> Scores { get; } = new();
    public Dictionary<int, int> TeamScores { get; } = new();
    public IReadOnlyDictionary<int, int> PlayerTeams => _playerTeams;
    public IReadOnlyDictionary<int, int> Bids => _bids;
    public IReadOnlyDictionary<int, IReadOnlyList<IReadOnlyList<Card>>> Melds => _melds.ToDictionary(pair => pair.Key, pair => (IReadOnlyList<IReadOnlyList<Card>>)pair.Value.ToArray());
    public GameStatistics Statistics { get; } = new();
    public bool IsComplete { get; set; }
    public IReadOnlyList<GameAction> History => _history.AsReadOnly();
    public int? WinnerIndex { get; private set; }
    // RNG seed used to initialize the session; preserved for deterministic replay/undo.
    public int Seed { get; internal set; } = 0;

    public CardPile AddPile(string name)
    {
        var pile = new CardPile(name);
        if (!_piles.TryAdd(name, pile))
            throw new InvalidOperationException($"Pile '{name}' already exists.");

        return pile;
    }

    public CardPile GetPile(string name)
    {
        if (!_piles.TryGetValue(name, out CardPile? pile))
            throw new KeyNotFoundException($"Pile '{name}' does not exist.");

        return pile;
    }

    public void AdvanceTurn()
    {
        int previousPlayer = CurrentPlayerIndex;
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerCount;
        Statistics.RecordTurn(previousPlayer);
        if (CurrentPlayerIndex == 0)
        {
            Round++;
            Statistics.RecordRoundCompleted();
        }
    }

    public void DealToEach(Deck deck, int cardsPerPlayer)
    {
        ArgumentNullException.ThrowIfNull(deck);
        if (cardsPerPlayer < 0)
            throw new ArgumentOutOfRangeException(nameof(cardsPerPlayer), "Cards per player cannot be negative.");

        for (int i = 0; i < cardsPerPlayer; i++)
            foreach (Hand hand in Hands)
                hand.AddCard(deck.Draw());
    }

    public void AssignTeams(IReadOnlyDictionary<int, int> playerTeams)
    {
        ArgumentNullException.ThrowIfNull(playerTeams);
        foreach ((int playerIndex, int teamIndex) in playerTeams)
        {
            if (playerIndex < 0 || playerIndex >= PlayerCount)
                throw new ArgumentOutOfRangeException(nameof(playerTeams), $"Player {playerIndex} is outside the session player range.");
            if (teamIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(playerTeams), "Team index cannot be negative.");
        }

        _playerTeams.Clear();
        foreach ((int playerIndex, int teamIndex) in playerTeams)
            _playerTeams[playerIndex] = teamIndex;
    }

    public void AssignAlternatingTeams(int teamCount)
    {
        if (teamCount < 1)
            throw new ArgumentOutOfRangeException(nameof(teamCount), "At least one team is required.");

        AssignTeams(Enumerable.Range(0, PlayerCount).ToDictionary(player => player, player => player % teamCount));
    }

    public void RecordAction(int playerIndex, Rules.MoveDescriptor move, MoveResult result)
    {
        ArgumentNullException.ThrowIfNull(move);
        ArgumentNullException.ThrowIfNull(result);

        _history.Add(new GameAction(_history.Count + 1, playerIndex, move, result, DateTimeOffset.UtcNow));
        Statistics.RecordMove(playerIndex, result.Succeeded);
        if (IsComplete)
            Statistics.Complete();
    }

    public MoveResult ApplyGenericAction(Rules.MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(move);
        int playerIndex = move.PlayerIndex ?? CurrentPlayerIndex;
        if (playerIndex != CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");

        if (move.Type.Equals("pass", StringComparison.OrdinalIgnoreCase))
        {
            AdvanceTurn();
            return MoveResult.Success("Player passed.");
        }

        if (move.Type.Equals("bid", StringComparison.OrdinalIgnoreCase))
        {
            if (move.Bid is null || move.Bid < 0)
                return MoveResult.Failure("Bid must be zero or greater.");

            _bids[playerIndex] = move.Bid.Value;
            AdvanceTurn();
            return MoveResult.Success($"Player bid {move.Bid.Value}.");
        }

        if (move.Type.Equals("meld", StringComparison.OrdinalIgnoreCase))
        {
            if (move.Cards is null || move.Cards.Count == 0)
                return MoveResult.Failure("Meld requires one or more cards.");

            Card[] cards = move.Cards
                .Select(cardName => Hands[playerIndex].Cards.FirstOrDefault(card => card.ToString().Equals(cardName, StringComparison.OrdinalIgnoreCase)))
                .OfType<Card>()
                .ToArray();
            if (cards.Length != move.Cards.Count)
                return MoveResult.Failure("All meld cards must be in the player's hand.");

            if (!_melds.TryGetValue(playerIndex, out List<IReadOnlyList<Card>>? melds))
            {
                melds = new List<IReadOnlyList<Card>>();
                _melds[playerIndex] = melds;
            }

            melds.Add(cards);
            RecordScore(playerIndex, cards.Sum(card => (int)card.Rank));
            return MoveResult.Success($"Player melded {cards.Length} card(s).");
        }

        if (move.Type.Equals("knock", StringComparison.OrdinalIgnoreCase))
        {
            IsComplete = true;
            DetermineWinnerByHighScore();
            return MoveResult.Success("Player knocked to end the hand.");
        }

        return MoveResult.Failure($"Unsupported generic action '{move.Type}'.");
    }

    public void StartNextRound()
    {
        foreach (Hand hand in Hands)
            hand.Clear();
        foreach (CardPile pile in _piles.Values)
            pile.Clear();

        _history.Clear();
        _bids.Clear();
        _melds.Clear();
        CurrentPlayerIndex = 0;
        Round++;
        IsComplete = false;
    }

    public void RestoreState(
        int currentPlayerIndex,
        int round,
        bool isComplete,
        IReadOnlyDictionary<int, int> scores,
        IReadOnlyDictionary<int, int> teamScores,
        IReadOnlyDictionary<int, int> playerTeams,
        IReadOnlyDictionary<int, IReadOnlyList<Card>> hands,
        IReadOnlyDictionary<string, IReadOnlyList<Card>> piles)
    {
        if (currentPlayerIndex < 0 || currentPlayerIndex >= PlayerCount)
            throw new ArgumentOutOfRangeException(nameof(currentPlayerIndex), "Current player index is outside the player range.");
        if (round < 1)
            throw new ArgumentOutOfRangeException(nameof(round), "Round must be one or greater.");

        foreach (Hand hand in Hands)
            hand.Clear();
        foreach (CardPile pile in _piles.Values)
            pile.Clear();

        Scores.Clear();
        TeamScores.Clear();
        _playerTeams.Clear();
        _history.Clear();

        CurrentPlayerIndex = currentPlayerIndex;
        Round = round;
        IsComplete = isComplete;

        foreach ((int playerIndex, int score) in scores)
            Scores[playerIndex] = score;
        foreach ((int teamIndex, int score) in teamScores)
            TeamScores[teamIndex] = score;
        foreach ((int playerIndex, int teamIndex) in playerTeams)
            _playerTeams[playerIndex] = teamIndex;
        foreach ((int playerIndex, IReadOnlyList<Card> cards) in hands)
        {
            if (playerIndex < 0 || playerIndex >= PlayerCount)
                throw new ArgumentOutOfRangeException(nameof(hands), $"Player {playerIndex} is outside the session player range.");
            foreach (Card card in cards)
                Hands[playerIndex].AddCard(card);
        }
        foreach ((string pileName, IReadOnlyList<Card> cards) in piles)
        {
            CardPile pile = _piles.TryGetValue(pileName, out CardPile? existing) ? existing : AddPile(pileName);
            foreach (Card card in cards)
                pile.Add(card);
        }
    }

    public void RecordDraw(int playerIndex, string pileName, int count)
    {
        Statistics.RecordDraw(playerIndex, count);
        Statistics.RecordPileRemoved(pileName, count);
    }

    public void RecordPlay(int playerIndex, string destinationPileName, int count)
    {
        Statistics.RecordPlay(playerIndex, count);
        Statistics.RecordPileAdded(destinationPileName, count, GetPile(destinationPileName).Count);
    }

    public void RecordDiscard(int playerIndex, string destinationPileName, int count)
    {
        Statistics.RecordDiscard(playerIndex, count);
        Statistics.RecordPileAdded(destinationPileName, count, GetPile(destinationPileName).Count);
    }

    public void RecordCapture(int playerIndex, string destinationPileName, int count)
    {
        Statistics.RecordCapture(playerIndex, count);
        Statistics.RecordPileAdded(destinationPileName, count, GetPile(destinationPileName).Count);
    }

    public void RecordClear(int playerIndex, string destinationPileName, int count)
    {
        Statistics.RecordClear(playerIndex, count);
        Statistics.RecordPileAdded(destinationPileName, count, GetPile(destinationPileName).Count);
    }

    public void RecordScore(int playerIndex, int score)
    {
        Statistics.RecordScore(playerIndex, score);
        if (_playerTeams.TryGetValue(playerIndex, out int teamIndex))
            TeamScores[teamIndex] = TeamScores.GetValueOrDefault(teamIndex) + score;
    }

    public int? DetermineWinnerByHighScore()
    {
        if (TeamScores.Count > 0)
        {
            int bestTeamScore = TeamScores.Values.Max();
            int[] winningTeams = TeamScores.Where(pair => pair.Value == bestTeamScore).Select(pair => pair.Key).ToArray();
            if (winningTeams.Length == 1)
            {
                int winningTeam = winningTeams[0];
                int[] players = _playerTeams.Where(pair => pair.Value == winningTeam).Select(pair => pair.Key).ToArray();
                WinnerIndex = players.Length == 1 ? players[0] : null;
                return WinnerIndex;
            }
        }

        if (Scores.Count == 0)
            return null;

        int bestScore = Scores.Values.Max();
        int[] winners = Scores.Where(pair => pair.Value == bestScore).Select(pair => pair.Key).ToArray();
        WinnerIndex = winners.Length == 1 ? winners[0] : null;
        return WinnerIndex;
    }

    public int? DetermineWinnerByEmptyHand()
    {
        int[] winners = Hands
            .Select((hand, index) => new { hand, index })
            .Where(item => item.hand.IsEmpty)
            .Select(item => item.index)
            .ToArray();

        WinnerIndex = winners.Length == 1 ? winners[0] : null;
        return WinnerIndex;
    }

    public GameSnapshot CreateSnapshot()
    {
        return new GameSnapshot(
            GameName,
            PlayerCount,
            CurrentPlayerIndex,
            Round,
            IsComplete,
            WinnerIndex,
            Scores.ToDictionary(pair => pair.Key, pair => pair.Value),
            TeamScores.ToDictionary(pair => pair.Key, pair => pair.Value),
            _playerTeams.ToDictionary(pair => pair.Key, pair => pair.Value),
            Hands.Select((hand, index) => new KeyValuePair<int, IReadOnlyList<Card>>(index, hand.Cards.ToArray())).ToDictionary(),
            _piles.ToDictionary(pair => pair.Key, pair => (IReadOnlyList<Card>)pair.Value.Cards.ToArray(), StringComparer.OrdinalIgnoreCase));
    }

    public StatisticsSnapshot CreateStatisticsSnapshot()
    {
        return new StatisticsSnapshot(
            Statistics.StartedAt,
            Statistics.CompletedAt,
            Statistics.Elapsed,
            Statistics.MovesAttempted,
            Statistics.MovesSucceeded,
            Statistics.MovesFailed,
            Statistics.SuccessRate,
            Statistics.TotalCardsDrawn,
            Statistics.TotalCardsPlayed,
            Statistics.TotalCardsDiscarded,
            Statistics.TotalCardsCaptured,
            Statistics.TotalCardsCleared,
            Statistics.TotalScoreGained,
            Statistics.TurnsTaken,
            Statistics.RoundsCompleted,
            Statistics.LongestFailedMoveStreak,
            Statistics.Players.ToDictionary(
                pair => pair.Key,
                pair => new PlayerStatisticsSnapshot(
                    pair.Value.CardsDrawn,
                    pair.Value.CardsPlayed,
                    pair.Value.CardsDiscarded,
                    pair.Value.CardsCaptured,
                    pair.Value.CardsCleared,
                    pair.Value.MovesAttempted,
                    pair.Value.MovesSucceeded,
                    pair.Value.MovesFailed,
                    pair.Value.SuccessRate,
                    pair.Value.TurnsTaken,
                    pair.Value.ScoreGained)),
            Statistics.Piles.ToDictionary(
                pair => pair.Key,
                pair => new PileStatisticsSnapshot(pair.Value.PileName, pair.Value.CardsAdded, pair.Value.CardsRemoved, pair.Value.PeakCount),
                StringComparer.OrdinalIgnoreCase));
    }
}
