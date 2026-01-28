using PanKunik.Results.Enums;

namespace PanKunik.Results;

public sealed record Error(string Code, string Message, ErrorType Type)
{
    public static Error Failure(string code, string message)
        => new(code, message, ErrorType.Failure);

    public static Error Validation(string code, string message)
        => new(code, message, ErrorType.Validation);

    public static Error NotFound(string code, string message)
        => new(code, message, ErrorType.NotFound);
}