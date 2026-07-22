using System;
using System.Collections.Generic;
using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class AccordionRules : ICardGameRules
{
    public string Name => "Accordion";
    public int MinPlayers => 1;
    public int MaxPlayers => 1;

    public CardGameSession CreateSession(int playerCount, Random? random = null)
    {
        if (playerCount != 1)
            throw new ArgumentOutOfRangeException(nameof(playerCount), "Accordion is a solitaire.");

        random ??= new Random();
        var session = new CardGameSession(Name, 1);
        var deck = new Deck();
        deck.Shuffle(random);

        // Deal: each card becomes a pile (single-card piles in a row)
        int index = 0;
        while (!deck.IsEmpty)
        {
            var pileName = $"pile-{index}";
            var pile = session.AddPile(pileName);
            pile.Add(deck.Draw());
            index++;
        }

        session.CurrentPlayerIndex = 0;
        return session;
    }

    public IReadOnlyList<MoveDescriptor> GetLegalMoves(CardGameSession session)
    {
        var moves = new List<MoveDescriptor>();
        var piles = session.Piles.Keys
            .Select(name => new { name, index = int.TryParse(name.Substring(name.LastIndexOf('-') + 1), out var v) ? v : 0 })
            .OrderBy(p => p.index)
            .Select(p => session.GetPile(p.name))
            .ToList();

        for (int i = 0; i < piles.Count; i++)
        {
            var src = piles[i];
            if (src.Count == 0) continue;
            var top = src.Peek();

            // Move onto previous non-empty pile to the left
            int prev = -1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (piles[j].Count > 0) { prev = j; break; }
            }
            if (prev >= 0)
            {
                var dest = piles[prev];
                if (Matches(dest.Peek(), top))
                                    moves.Add(new MoveDescriptor("move", src.Name, dest.Name, 1, Cards: new[] { top.ToString() }));
            }

            // Move onto pile three to the left (non-empty)
            int third = -1;
            int found = 0;
            for (int j = i - 1; j >= 0 && found < 3; j--)
            {
                if (piles[j].Count > 0) found++;
                if (found == 3) { third = j; break; }
            }
            if (third >= 0)
            {
                var dest = piles[third];
                if (Matches(dest.Peek(), top))
                                    moves.Add(new MoveDescriptor("move", src.Name, dest.Name, 1, Cards: new[] { top.ToString() }));
            }
        }

        return moves;
    }

    public MoveResult ApplyMove(CardGameSession session, MoveDescriptor move)
    {
        if (move.Type != "move") return MoveResult.Failure("Unsupported move type.");
        var src = session.GetPile(move.Source);
        var dest = session.GetPile(move.Destination ?? string.Empty);
        if (src is null || dest is null) return MoveResult.Failure("Pile not found.");
        if (src.Count == 0) return MoveResult.Failure("Source empty.");

        var card = src.Draw();
        dest.Add(card);

        // We treat empty piles as removed for the purposes of moves; session retains empty piles but GetLegalMoves skips empties
        // Check win: only one non-empty pile remains
        int nonEmpty = session.Piles.Values.Count(p => p.Count > 0);
        if (nonEmpty == 1)
        {
            session.IsComplete = true;
        }

        return MoveResult.Success("Moved.");
    }

    private static bool Matches(Card a, Card b)
    {
        return a.Rank == b.Rank || a.Suit == b.Suit;
    }
}
