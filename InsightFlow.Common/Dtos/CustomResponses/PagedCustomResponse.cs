using System.Collections;
using System.Net;

namespace InsightFlow.Common.Dtos.CustomResponses;

public class PagedCustomResponse<T> : CustomResponse<T> where T : IEnumerable?
{
    public int TotalCount { get; set; }

    public int CurrentCount { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public new static PagedCustomResponse<T> CreateUnsuccessfulResponse(HttpStatusCode httpStatusCode, string? message = null) =>
        new()
        {
            IsSuccess = false,
            Message = message,
            HttpStatusCode = httpStatusCode
        };

    public static PagedCustomResponse<T> CreateSuccessfulResponse(
        T? data,
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK,
        int totalCount = 0,
        int currentCount = 0,
        int pageNumber = 0,
        int pageSize = 0) => new()
    {
        Data = data,
        IsSuccess = true,
        Message = message,
        HttpStatusCode = httpStatusCode,
        TotalCount = totalCount,
        CurrentCount = currentCount,
        PageNumber = pageNumber,
        PageSize = pageSize
    };
}