using Cards.Core.Engine;
using Cards.Core.Engine.Rules;
using Xunit;

namespace Cards.Tests;

public class CardGameEngineTests
{
    [Fact]
    public void StartGame_Klondike_CreatesExpectedPiles()
    {
        var engine = new CardGameEngine();

        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        Assert.Equal("Klondike", session.GameName);
        Assert.Equal(13, session.Piles.Count);
        Assert.Equal(24, session.GetPile("stock").Count);
        Assert.Equal(7, session.GetPile("tableau7").Count);
    }

    [Fact]
    public void Klondike_DrawMove_MovesCardFromStockToWaste()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        MoveResult result = engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        Assert.True(result.Succeeded);
        Assert.Equal(23, session.GetPile("stock").Count);
        Assert.Equal(1, session.GetPile("waste").Count);
    }

    [Fact]
    public void StartGame_Euchre_DealsFiveCardsToEachPlayer()
    {
        var engine = new CardGameEngine();

        CardGameSession session = engine.StartGame("Euchre", 4, new Random(42));

        Assert.All(session.Hands, hand => Assert.Equal(5, hand.Count));
        Assert.True(session.GetPile("stock").Count >= 0);
    }

    [Fact]
    public void TrickTaking_PlayMove_AddsCardToTrickAndAdvancesTurn()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));
        MoveDescriptor move = engine.GetLegalMoves(session)[0];

        MoveResult result = engine.ApplyMove(session, move);

        Assert.True(result.Succeeded);
        Assert.Equal(1, session.GetPile("trick").Count);
        Assert.Equal(1, session.CurrentPlayerIndex);
        Assert.Equal(12, session.Hands[0].Count);
    }

    [Fact]
    public void StartGame_UnknownGame_Throws()
    {
        var engine = new CardGameEngine();

        Assert.Throws<KeyNotFoundException>(() => engine.StartGame("Unknown", 2));
    }

    [Fact]
    public void SupportedGames_IncludesMultipleRuleFamilies()
    {
        var engine = new CardGameEngine();

        Assert.Contains("Spider", engine.SupportedGames);
        Assert.Contains("Pyramid", engine.SupportedGames);
        Assert.Contains("Gin Rummy", engine.SupportedGames);
        Assert.Contains("Crazy Eights", engine.SupportedGames);
    }

    [Fact]
    public void MatchingSolitaire_ClearPair_RemovesCardsThatHitTarget()
    {
        var rules = new MatchingSolitaireRules("Test Match", tableauCount: 2);
        CardGameSession session = rules.CreateSession(1, new Random(42));
        session.GetPile("tableau1").Clear();
        session.GetPile("tableau2").Clear();
        session.GetPile("tableau1").Add(new Cards.Core.Models.Card(Cards.Core.Models.Suit.Clubs, Cards.Core.Models.Rank.Six));
        session.GetPile("tableau2").Add(new Cards.Core.Models.Card(Cards.Core.Models.Suit.Hearts, Cards.Core.Models.Rank.Seven));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("clear-pair", "tableau1", "tableau2", 2));

        Assert.True(result.Succeeded);
        Assert.True(session.GetPile("tableau1").IsEmpty);
        Assert.True(session.GetPile("tableau2").IsEmpty);
        Assert.Equal(2, session.GetPile("cleared").Count);
    }

    [Fact]
    public void DrawDiscard_DrawAndDiscard_ChangesHandAndTurn()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Gin Rummy", 2, new Random(42));
        int startingHand = session.Hands[0].Count;

        MoveResult draw = engine.ApplyMove(session, new MoveDescriptor("draw", "stock", PlayerIndex: 0));
        MoveDescriptor discardMove = engine.GetLegalMoves(session).First(move => move.Type == "discard");
        MoveResult discard = engine.ApplyMove(session, discardMove);

        Assert.True(draw.Succeeded);
        Assert.True(discard.Succeeded);
        Assert.Equal(startingHand, session.Hands[0].Count);
        Assert.Equal(1, session.CurrentPlayerIndex);
    }

    [Fact]
    public void SheddingRules_AlwaysAllowsDrawWhenNoPlayableCardExists()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Crazy Eights", 2, new Random(42));

        Assert.Contains(engine.GetLegalMoves(session), move => move.Type == "draw");
    }

    [Fact]
    public void ApplyMove_RecordsActionHistory()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        GameAction action = Assert.Single(session.History);
        Assert.Equal(1, action.Sequence);
        Assert.True(action.Result.Succeeded);
    }

    [Fact]
    public void CreateSnapshot_CopiesVisibleState()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Euchre", 4, new Random(42));

        GameSnapshot snapshot = session.CreateSnapshot();

        Assert.Equal("Euchre", snapshot.GameName);
        Assert.Equal(4, snapshot.PlayerCount);
        Assert.Equal(5, snapshot.Hands[0].Count);
        Assert.True(snapshot.Piles["stock"].Count >= 0);
    }

    [Fact]
    public void Casino_StartGame_CreatesTableAndCaptureMoves()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Casino", 2, new Random(42));

        Assert.Equal(4, session.GetPile("table").Count);
        Assert.Equal(4, session.Hands[0].Count);
        Assert.NotEmpty(engine.GetLegalMoves(session));
    }

    [Fact]
    public void Statistics_KlondikeDraw_TracksMoveAndCardFlow()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        var statistics = session.CreateStatisticsSnapshot();
        Assert.Equal(1, statistics.MovesAttempted);
        Assert.Equal(1, statistics.MovesSucceeded);
        Assert.Equal(1, statistics.TotalCardsDrawn);
        Assert.Equal(1, statistics.Players[0].CardsDrawn);
        Assert.Equal(1, statistics.Piles["stock"].CardsRemoved);
        Assert.Equal(1, statistics.Piles["waste"].CardsAdded);
    }

    [Fact]
    public void Statistics_FailedMove_TracksFailureStreak()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Klondike", 1, new Random(42));

        engine.ApplyMove(session, new MoveDescriptor("move", "waste", "foundation1"));

        var statistics = session.CreateStatisticsSnapshot();
        Assert.Equal(1, statistics.MovesAttempted);
        Assert.Equal(1, statistics.MovesFailed);
        Assert.Equal(1, statistics.LongestFailedMoveStreak);
        Assert.Equal(0, statistics.SuccessRate);
    }

    [Fact]
    public void Statistics_TrickTakingPlay_TracksPlayedScoreAndTurn()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Bridge", 4, new Random(42));
        MoveDescriptor move = engine.GetLegalMoves(session)[0];

        engine.ApplyMove(session, move);

        var statistics = session.CreateStatisticsSnapshot();
        Assert.Equal(1, statistics.TotalCardsPlayed);
        Assert.True(statistics.MovesSucceeded > 0);
        Assert.Equal(1, statistics.TurnsTaken);
        Assert.Equal(1, statistics.Piles["trick"].CardsAdded);
    }

    [Fact]
    public void Statistics_DrawDiscard_TracksDrawDiscardAndScores()
    {
        var engine = new CardGameEngine();
        CardGameSession session = engine.StartGame("Gin Rummy", 2, new Random(42));

        engine.ApplyMove(session, new MoveDescriptor("draw", "stock", PlayerIndex: 0));
        MoveDescriptor discardMove = engine.GetLegalMoves(session).First(move => move.Type == "discard");
        engine.ApplyMove(session, discardMove);

        var statistics = session.CreateStatisticsSnapshot();
        Assert.Equal(1, statistics.TotalCardsDrawn);
        Assert.Equal(1, statistics.TotalCardsDiscarded);
        Assert.True(statistics.TotalScoreGained > 0);
        Assert.Equal(1, statistics.TurnsTaken);
    }

    [Fact]
    public void Statistics_MatchingSolitaire_TracksClearedCards()
    {
        var rules = new MatchingSolitaireRules("Test Match", tableauCount: 2);
        CardGameSession session = rules.CreateSession(1, new Random(42));
        session.GetPile("tableau1").Clear();
        session.GetPile("tableau2").Clear();
        session.GetPile("tableau1").Add(new Cards.Core.Models.Card(Cards.Core.Models.Suit.Clubs, Cards.Core.Models.Rank.Six));
        session.GetPile("tableau2").Add(new Cards.Core.Models.Card(Cards.Core.Models.Suit.Hearts, Cards.Core.Models.Rank.Seven));

        rules.ApplyMove(session, new MoveDescriptor("clear-pair", "tableau1", "tableau2", 2));

        var statistics = session.CreateStatisticsSnapshot();
        Assert.Equal(2, statistics.TotalCardsCleared);
        Assert.Equal(2, statistics.Piles["cleared"].CardsAdded);
    }
}
