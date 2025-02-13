namespace InsightFlow.Domain.Common;

public record DomainResponse
{
    public DomainResponse(Error? error = null, string? message = null)
    {
        Error = error ?? Error.None;
        Message = message;
        IsSuccess = Error == Error.None;
    }

    public string? Message { get; }

    public Error Error { get; }

    public bool IsSuccess { get; init; }

    public bool IsFailure => !IsSuccess;
}

public record DomainResponse<T> : DomainResponse where T : class
{
    public DomainResponse(T? data, Error? error = null, string? message = null) : base(error, message)
    {
        Data = data;
    }

    public T? Data { get; }

    public static DomainResponse<T> CreateFailure(Error error, string? message = null) =>
        new(null, error, message)
        {
            IsSuccess = false
        };
}