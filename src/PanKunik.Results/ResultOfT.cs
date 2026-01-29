namespace PanKunik.Results;

/// <summary>
/// Represents the result of an operation that returns a value of type `TValue`.
/// </summary>
public sealed class Result<TValue> : Result
    where TValue : notnull
{
    private Result(TValue value)
        : base() => Value = value;
    private Result(Error error)
        : base(error) => Value = default;
    
    /// <summary>
    /// The value of the result. Throws if the result is a failure.
    /// </summary>
    public TValue Value
    {
        get
        {
            if (IsFailure || field is null)
                throw new InvalidOperationException("Cannot access the value of a failure result.");
            return field;
        }
    }
    
    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    public static Result<TValue> Success(TValue value) => new(value);
    
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    public new static Result<TValue> Failure(Error error) => new(error);

    /// <summary>
    /// Applies one of the two callbacks depending on whether the result is a success or failure.
    /// </summary>
    /// <typeparam name="TResult">The return type of the callbacks.</typeparam>
    /// <param name="onSuccess">Callback invoked if the result is success.</param>
    /// <param name="onFailure">Callback invoked if the result is failure.</param>
    /// <returns>The result of the invoked callback.</returns>
    public TResult Match<TResult>(
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure
    )
    {
        return IsSuccess
            ? onSuccess(Value)
            : onFailure(Error!);
    }

    [Obsolete("Use `Match()` instead.")]
    public TResult Map<TResult>(
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure
    )
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error!);
    }
}