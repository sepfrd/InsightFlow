namespace InsightFlow.DataAccess.Dtos;

public class PagedEntitiesResponseDto<T>
{
    public PagedEntitiesResponseDto(List<T>? entities = null, int totalCount = 0)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    public List<T>? Entities { get; }

    public int TotalCount { get; }
}