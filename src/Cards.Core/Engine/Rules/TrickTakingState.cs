using Cards.Core.Models;

namespace Cards.Core.Engine.Rules;

public sealed class TrickTakingState
{
    private readonly List<TrickPlay> _currentTrick = new();

    public Suit? TrumpSuit { get; set; }
    public Suit? LedSuit => _currentTrick.Count == 0 ? null : _currentTrick[0].Card.Suit;
    public int? DeclarerIndex { get; set; }
    public int? ContractLevel { get; set; }
    public IReadOnlyList<TrickPlay> CurrentTrick => _currentTrick.AsReadOnly();

    public void AddPlay(int playerIndex, Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        _currentTrick.Add(new TrickPlay(playerIndex, card));
    }

    public void ClearTrick() => _currentTrick.Clear();
}
