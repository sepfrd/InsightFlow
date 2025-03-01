namespace InsightFlow.Application.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> values, int pageNumber, int pageSize) =>
        values
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
}