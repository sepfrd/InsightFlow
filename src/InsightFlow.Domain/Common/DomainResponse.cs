namespace InsightFlow.Domain.Common;

public record DomainResponse
{
    protected DomainResponse(string? message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
        IsSuccess = statusCode >= 200 && statusCode < 300;
    }

    public string? Message { get; init; }

    public int StatusCode { get; init; }

    public bool IsSuccess { get; init; }

    public static DomainResponse CreateBaseSuccess(string? message, int resultCode) =>
        new(message, resultCode);

    public static DomainResponse CreateBaseFailure(string message, int resultCode) =>
        new(message, resultCode);
}

public record DomainResponse<T> : DomainResponse
{
    protected DomainResponse(string? message, int statusCode, T? data) : base(message, statusCode)
    {
        Data = data;
    }

    public T? Data { get; init; }

    public static DomainResponse<T> CreateSuccess(string? message, int resultCode, T data) =>
        new(message, resultCode, data);

    public static DomainResponse<T> CreateFailure(string message, int resultCode) =>
        new(message, resultCode, default);
}