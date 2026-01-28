namespace PanKunik.Results;

public sealed class Result<TIn>
    : Result
{
    private Result(TIn value)
        : base()
    {
        Value = value;
    }

    private Result(Error error)
        : base(error)
    {
        Value = default;
    }

    public TIn? Value { get; }

    public static Result<TIn> Success(TIn value) => new(value);
    public new static Result<TIn> Failure(Error error) => new(error);

    public TResult Map<TResult>(
        Func<TIn, TResult> onSuccess,
        Func<Error, TResult> onFailure
    )
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
    }
}

public class Result
{
    protected Result()
    {
        Error = null;
    }

    protected Result(Error error)
    {
        Error = error;
    }

    public Error? Error { get; }
    public bool IsSuccess => Error == null;
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);

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