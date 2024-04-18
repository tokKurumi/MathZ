namespace MathZ.Shared.Pagination;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;

        AddRange(items);
    }

    [JsonProperty]
    public int PageNumber { get; private set; }

    [JsonProperty]
    public int PageSize { get; private set; }

    [JsonProperty]
    public int TotalCount { get; private set; }

    [JsonProperty]
    public IEnumerable<T> Data => ToArray();

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedList<T>(items, pageNumber, pageSize, totalCount);
    }

    public static Task<PagedList<T>> CreateAsync(IQueryable<T> source, PaginationQuery<T> request, CancellationToken cancellationToken = default)
    {
        return CreateAsync(source, request.PageNumber, request.PageSize, cancellationToken);
    }
}
