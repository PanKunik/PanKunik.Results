namespace PanKunik.Results;

/// <summary>
/// Represents the result of an operation that does not return a value.
/// </summary>
public class Result
{
    private protected Result() => Error = null;
    private protected Result(Error error) => Error = error;
    
    /// <summary>
    /// The error associated with a failed result. Null if success.
    /// </summary>
    public Error? Error { get; }
    
    /// <summary>
    /// True if the result represents a successful operation.
    /// </summary>
    public bool IsSuccess => Error == null;
    
    /// <summary>
    /// True if the result represents a failed operation.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new();
    
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    public static Result Failure(Error error) => new(error);

    /// <summary>
    /// Applies one of the two callbacks depending on whether the result is a success or failure.
    /// </summary>
    /// <param name="onSuccess">Callback invoked if the result is success.</param>
    /// <param name="onFailure">Callback invoked if the result is failure.</param>
    /// <returns>The result of the invoked callback.</returns>
    public TResult Match<TResult>(
        Func<TResult> onSuccess,
        Func<Error, TResult> onFailure
    )
    {
        return IsSuccess
            ? onSuccess()
            : onFailure(Error!);
    }

    [Obsolete("Use `Match()` instead.")]
    public TResult Map<TResult>(
        Func<TResult> onSuccess,
        Func<Error, TResult> onFailure
    )
    {
        return IsSuccess
            ? onSuccess()
            : onFailure(Error!);
    }
}