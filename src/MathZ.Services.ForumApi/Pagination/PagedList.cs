namespace MathZ.Services.ForumApi.Pagination;

using Microsoft.EntityFrameworkCore;

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;

        AddRange(items);
    }

    public int PageNumber { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, pageNumber, pageSize, totalCount);
    }

    public static Task<PagedList<T>> CreateAsync(IQueryable<T> source, PaginationQuery<T> request)
    {
        return CreateAsync(source, request.PageNumber, request.PageSize);
    }
}
