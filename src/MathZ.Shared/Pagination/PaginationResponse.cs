namespace MathZ.Shared.Pagination;

public class PaginationResponse<T>(IEnumerable<T> data, int total, int pageNumber)
{
    public IEnumerable<T> Data { get; } = data;

    public int Total { get; } = total;

    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = data.Count();

    public static PaginationResponse<T> Create(IEnumerable<T> data, int total, int pageNumber)
    {
        return new PaginationResponse<T>(data, total, pageNumber);
    }
}
