namespace Cards.Core.Engine.Validation;

public sealed record ValidationResult(IReadOnlyList<ValidationDiagnostic> Diagnostics)
{
    public bool IsValid => Diagnostics.All(diagnostic => diagnostic.Severity != ValidationSeverity.Error);

    public static ValidationResult Valid { get; } = new(Array.Empty<ValidationDiagnostic>());
}
