using Cards.Core.Engine;
using Cards.Core.Engine.Profiles;
using Cards.Core.Engine.Rules;
using Xunit;

namespace Cards.Tests;

public class DeepSolitaireRulesTests
{
    [Fact]
    public void Klondike_Setup_HasOnlyTopTableauCardsFaceUp()
    {
        var rules = new KlondikeRules();

        CardGameSession session = rules.CreateSession(1, new Random(42));

        CardPile tableau7 = session.GetPile("tableau7");
        Assert.Equal(7, tableau7.Count);
        Assert.False(tableau7.VisibleCards[0].IsFaceUp);
        Assert.True(tableau7.VisibleCards[^1].IsFaceUp);
    }

    [Fact]
    public void Klondike_Redeal_RecyclesWasteIntoStock()
    {
        var rules = new KlondikeRules();
        CardGameSession session = rules.CreateSession(1, new Random(42));
        while (!session.GetPile("stock").IsEmpty)
            rules.ApplyMove(session, new MoveDescriptor("draw", "stock", "waste"));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("redeal", "waste", "stock"));

        Assert.True(result.Succeeded);
        Assert.Equal(24, session.GetPile("stock").Count);
        Assert.True(session.GetPile("waste").IsEmpty);
    }

    [Fact]
    public void FreeCell_Setup_CreatesCellsAndDealsAllCards()
    {
        var rules = new FreeCellRules();

        CardGameSession session = rules.CreateSession(1, new Random(42));

        Assert.Equal(4, session.Piles.Keys.Count(name => name.StartsWith("cell")));
        Assert.Equal(52, session.Piles.Values.Where(pile => pile.Name.StartsWith("tableau")).Sum(pile => pile.Count));
        Assert.Contains(rules.GetLegalMoves(session), move => move.Destination?.StartsWith("cell") == true);
    }

    [Fact]
    public void FreeCell_CanMoveToEmptyCell()
    {
        var rules = new FreeCellRules();
        CardGameSession session = rules.CreateSession(1, new Random(42));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("move", "tableau1", "cell1"));

        Assert.True(result.Succeeded);
        Assert.Single(session.GetPile("cell1").Cards);
    }

    [Fact]
    public void Spider_Setup_UsesTwoDecksAndFaceDownTableauCards()
    {
        var rules = new SpiderRules();

        CardGameSession session = rules.CreateSession(1, new Random(42));

        Assert.Equal(50, session.GetPile("stock").Count);
        Assert.Equal(54, session.Piles.Values.Where(pile => pile.Name.StartsWith("tableau")).Sum(pile => pile.Count));
        Assert.False(session.GetPile("tableau1").VisibleCards[0].IsFaceUp);
    }

    [Fact]
    public void Spider_DealRow_AddsOneCardToEachTableau()
    {
        var rules = new SpiderRules();
        CardGameSession session = rules.CreateSession(1, new Random(42));

        MoveResult result = rules.ApplyMove(session, new MoveDescriptor("deal-row", "stock", "tableau", 10));

        Assert.True(result.Succeeded);
        Assert.Equal(40, session.GetPile("stock").Count);
        Assert.Equal(64, session.Piles.Values.Where(pile => pile.Name.StartsWith("tableau")).Sum(pile => pile.Count));
    }

    [Fact]
    public void ProfileCatalog_UsesDeepRulesForFreeCellAndSpider()
    {
        Assert.IsType<FreeCellRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["FreeCell"]));
        Assert.IsType<SpiderRules>(RuleProfileCatalog.CreateRules(RuleProfileCatalog.ByGameName["Spider"]));
    }
}
