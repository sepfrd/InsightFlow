using System.Collections;
using System.Net;
using InsightFlow.Common.Constants;

namespace InsightFlow.Common.Dtos.CustomResponses;

public class PagedCustomResponse<T> : CustomResponse<T> where T : IEnumerable?
{
    public int TotalCount { get; init; }

    public int CurrentCount { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

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
        int pageNumber = 1,
        int pageSize = ApplicationConstants.MinimumPageSize) => new()
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