namespace PanKunik.Results;

public static class ErrorExtensions
{
    extension(Error error)
    {
        public Result<T> ToFailure<T>()
            where T : notnull
            => Result<T>.Failure(error);
    }
}