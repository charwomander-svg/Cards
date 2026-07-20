using Cards.Core.Engine.Rules;

namespace Cards.Core.Engine.Validation;

public sealed class SessionValidator
{
    private readonly CardGameEngine _engine;

    public SessionValidator(CardGameEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    public ValidationResult ValidateSession(CardGameSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        var diagnostics = new List<ValidationDiagnostic>();

        if (session.PlayerCount < 1)
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Error, "players.none", "Session must have at least one player."));
        if (session.CurrentPlayerIndex < 0 || session.CurrentPlayerIndex >= session.PlayerCount)
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Error, "turn.invalid-player", "Current player index is outside the player range."));
        if (session.Hands.Count != session.PlayerCount)
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Error, "hands.count", "Hand count must match player count."));
        if (session.Piles.Keys.Any(string.IsNullOrWhiteSpace))
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Error, "piles.empty-name", "All piles must have names."));
        if (session.History.Count > 0 && session.History.Select(action => action.Sequence).Distinct().Count() != session.History.Count)
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Error, "history.duplicate-sequence", "Action history contains duplicate sequence numbers."));
        if (!session.IsComplete && _engine.GetLegalMoves(session).Count == 0)
            diagnostics.Add(new ValidationDiagnostic(ValidationSeverity.Warning, "moves.none", "Session has no legal moves but is not marked complete."));

        return diagnostics.Count == 0 ? ValidationResult.Valid : new ValidationResult(diagnostics);
    }

    public ValidationResult ValidateMove(CardGameSession session, MoveDescriptor move)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(move);

        IReadOnlyList<MoveDescriptor> legalMoves = _engine.GetLegalMoves(session);
        bool isLegal = legalMoves.Contains(move);
        return isLegal
            ? ValidationResult.Valid
            : new ValidationResult(new[]
            {
                new ValidationDiagnostic(ValidationSeverity.Error, "move.illegal", "Move is not currently legal for this session.")
            });
    }
}
