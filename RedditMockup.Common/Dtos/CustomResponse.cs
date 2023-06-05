using System.Net;

namespace RedditMockup.Common.Dtos;

public class CustomResponse<T>
{
    public T? Data { get; init; }

    public bool IsSuccess { get; init; }

    public string? Message { get; init; }

    public HttpStatusCode HttpStatusCode { get; init; }

    public static CustomResponse<T> CustomNotFoundResponse = new CustomResponse<T>
    {
        IsSuccess = false,
        HttpStatusCode = HttpStatusCode.NotFound
    };
}

public class CustomResponse
{
    public dynamic? Data { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }
    
    public HttpStatusCode HttpStatusCode { get; set; }
}