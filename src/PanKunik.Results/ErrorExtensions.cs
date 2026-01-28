namespace PanKunik.Results;

public static class ErrorExtensions
{
    extension(Error error)
    {
        public Result<T> ToFailure<T>()
            => Result<T>.Failure(error);
    }
}