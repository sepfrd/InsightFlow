namespace InsightFlow.Domain.Common;

public record PaginatedDomainResponse<T> : DomainResponse<T> where T : class, IEnumerable<object>
{
    private int _pageNumber;
    private int _pageSize;

    public PaginatedDomainResponse(
        int pageNumber,
        int pageSize,
        long? totalCount,
        T? data,
        Error? error = null,
        string? message = null)
        : base(data, error, message)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        CurrentPageCount = data?.Count();
    }

    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            if (value > 0)
            {
                _pageNumber = value;
            }
            else
            {
                throw new InvalidDataException("Page number must be greater than 0.");
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > 0)
            {
                _pageSize = value;
            }
            else
            {
                throw new InvalidDataException("Page size must be greater than 0.");
            }
        }
    }

    public long? TotalCount { get; set; }

    public long? CurrentPageCount { get; set; }

    public new static PaginatedDomainResponse<T> CreateFailure(Error error, string? message = null) =>
        new(0, 0, 0, null, error, message)
        {
            IsSuccess = false
        };
}