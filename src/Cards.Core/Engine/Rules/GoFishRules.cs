using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class GoFishRules : ICardGameRules
{
    public string Name => "Go Fish";
    public int MinPlayers => 2;
    public int MaxPlayers => 6;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, false, true, true, false, false, false, true, false, true, false, new[] { "request", "draw" });

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Go Fish supports 2-6 players.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);
        session.DealToEach(deck, playerCount <= 3 ? 7 : 5);
        session.AddPile("stock").AddRange(deck.Cards);
        session.AddPile("books");
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        int player = session.CurrentPlayerIndex;
        return session.Hands[player].Cards
            .Select(card => card.Rank)
            .Distinct()
            .SelectMany(rank => Enumerable.Range(0, session.PlayerCount)
                .Where(target => target != player)
                .Select(target => new MoveDescriptor("request", "hand", PlayerIndex: player, TargetPlayerIndex: target, RequestedRank: rank)))
            .ToArray();
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);
        int player = move.PlayerIndex ?? session.CurrentPlayerIndex;
        if (player != session.CurrentPlayerIndex)
            return MoveResult.Failure("It is not that player's turn.");
        if (!move.Type.Equals("request", StringComparison.OrdinalIgnoreCase))
            return MoveResult.Failure($"Unsupported Go Fish move '{move.Type}'.");
        if (move.TargetPlayerIndex is null || move.TargetPlayerIndex == player || move.TargetPlayerIndex < 0 || move.TargetPlayerIndex >= session.PlayerCount)
            return MoveResult.Failure("Request needs another target player.");
        if (move.RequestedRank is null)
            return MoveResult.Failure("Request needs a rank.");

        Hand asker = session.Hands[player];
        Hand target = session.Hands[move.TargetPlayerIndex.Value];
        Card[] matches = target.Cards.Where(card => card.Rank == move.RequestedRank).ToArray();
        if (matches.Length > 0)
        {
            foreach (Card card in matches)
            {
                target.PlayCard(card);
                asker.AddCard(card);
            }
            ScoreBooks(session, player);
            return MoveResult.Success($"Received {matches.Length} card(s).");
        }

        CardPile stock = session.GetPile("stock");
        if (!stock.IsEmpty)
        {
            asker.AddCard(stock.Draw());
            session.RecordDraw(player, "stock", 1);
        }
        ScoreBooks(session, player);
        session.AdvanceTurn();
        return MoveResult.Success("Go fish.");
    }

    private static void ScoreBooks(CardGameSession session, int player)
    {
        Hand hand = session.Hands[player];
        foreach (IGrouping<Rank, Card> group in hand.Cards.GroupBy(card => card.Rank).Where(group => group.Count() == 4).ToArray())
        {
            foreach (Card card in group.ToArray())
                hand.PlayCard(card);
            session.Scores[player] = session.Scores.GetValueOrDefault(player) + 1;
            session.RecordScore(player, 1);
        }

        session.IsComplete = session.Hands.All(hand => hand.IsEmpty) || session.GetPile("stock").IsEmpty && session.Hands.Any(hand => hand.IsEmpty);
        if (session.IsComplete)
            session.DetermineWinnerByHighScore();
    }
}
