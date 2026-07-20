namespace Cards.Core.Engine.Validation;

public sealed record ValidationDiagnostic(ValidationSeverity Severity, string Code, string Message);
