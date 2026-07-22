using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class CasinoRules : ICardGameRules
{
    public string Name { get; }
    public int MinPlayers => 2;
    public int MaxPlayers => 4;
    public RuleCapabilities Capabilities => new(Name, MinPlayers, MaxPlayers, MaxPlayers == 4, true, true, true, false, false, true, false, false, false, new[] { "trail", "capture" });

    public CasinoRules(string name = "Casino")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));

        Name = name;
    }

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount < MinPlayers || playerCount > MaxPlayers)
            throw new ArgumentOutOfRangeException(nameof(playerCount), $"{Name} supports {MinPlayers}-{MaxPlayers} players.");

        var session = new CardGameSession(Name, playerCount);
        var deck = new Deck();
        deck.Shuffle(random);
        session.DealToEach(deck, 4);
        session.AddPile("table");
        session.AddPile("captured");
        for (int i = 0; i < 4; i++)
            session.GetPile("table").Add(deck.Draw());
        session.AddPile("stock").AddRange(deck.Cards);
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        Hand hand = session.Hands[session.CurrentPlayerIndex];
        CardPile table = session.GetPile("table");
        var moves = new List<MoveDescriptor>();

        foreach (Card card in hand.Cards)
        {
            moves.Add(new MoveDescriptor("trail", card.ToString(), "table", PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString() }));
            foreach (Card tableCard in table.Cards.Where(tableCard => tableCard.Rank == card.Rank))
                moves.Add(new MoveDescriptor("capture", card.ToString(), tableCard.ToString(), PlayerIndex: session.CurrentPlayerIndex, Cards: new[] { card.ToString(), tableCard.ToString() }));
        }

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
        Card? handCard = hand.Cards.FirstOrDefault(card => card.ToString().Equals(move.Source, StringComparison.OrdinalIgnoreCase));
        if (handCard is null)
            return MoveResult.Failure("The selected card is not in the current player's hand.");

        CardPile table = session.GetPile("table");
        if (move.Type.Equals("trail", StringComparison.OrdinalIgnoreCase))
        {
            hand.PlayCard(handCard);
            table.Add(handCard);
            session.RecordPlay(playerIndex, "table", 1);
            session.AdvanceTurn();
            return MoveResult.Success($"{handCard} trailed to the table.");
        }

        if (move.Type.Equals("capture", StringComparison.OrdinalIgnoreCase))
        {
            Card? tableCard = table.Cards.FirstOrDefault(card => card.ToString().Equals(move.Destination, StringComparison.OrdinalIgnoreCase));
            if (tableCard is null || tableCard.Rank != handCard.Rank)
                return MoveResult.Failure("Capture requires a matching table card.");

            hand.PlayCard(handCard);
            table.Remove(tableCard);
            session.Statistics.RecordPileRemoved("table", 1);
            CardPile captured = session.GetPile("captured");
            captured.Add(handCard);
            captured.Add(tableCard);
            session.RecordCapture(playerIndex, "captured", 2);
            session.Scores[playerIndex] = session.Scores.GetValueOrDefault(playerIndex) + 2;
            session.RecordScore(playerIndex, 2);
            session.IsComplete = session.Hands.All(h => h.IsEmpty) && session.GetPile("stock").IsEmpty;
            if (session.IsComplete)
                session.DetermineWinnerByHighScore();
            session.AdvanceTurn();
            return MoveResult.Success($"{handCard} captured {tableCard}.");
        }

        return MoveResult.Failure($"Unsupported move '{move.Type}'.");
    }
}
