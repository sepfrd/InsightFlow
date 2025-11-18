namespace InsightFlow.Common.Results;

public class RomaakResult
{
    protected RomaakResult(bool success, string? message = null, Error? error = null)
    {
        Success = success;
        Message = message;
        Error = error;
    }

    public bool Success { get; init; }

    public string? Message { get; }

    public Error? Error { get; init; }

    public static RomaakResult CreateSuccess() => new(true);

    public static RomaakResult Failure(Error error, string? message = null) => new(false, message, error);
}

public class RomaakResult<T> : RomaakResult
{
    protected RomaakResult(T? body, bool success, string? message = null, Error? error = null)
        : base(success, message, error)
    {
        Success = success;
        Body = body;
        Error = error;
    }

    public T? Body { get; init; }

    public static RomaakResult<T> CreateSuccess(T value, string? message = null) => new(value, true, message);

    public static RomaakResult<T> FailureResult(Error error, string? message = null) => new(default, false, message, error);
}