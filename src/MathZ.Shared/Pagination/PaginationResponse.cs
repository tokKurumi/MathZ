namespace MathZ.Shared.Pagination;

public class PaginationResponse<T>(IEnumerable<T> data, int total, int pageNumber, int pageSize)
{
    public IEnumerable<T> Data { get; } = data;

    public int Total { get; } = total;

    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = pageSize;

    public int DataCount { get; } = data.Count();

    public static PaginationResponse<T> Create(IEnumerable<T> data, int total, int pageNumber, int pageSize)
    {
        return new PaginationResponse<T>(data, total, pageNumber, pageSize);
    }

    public static PaginationResponse<T> Create(IEnumerable<T> data, int total, PaginationRequest pagination)
    {
        return new PaginationResponse<T>(data, total, pagination.PageNumber, pagination.PageSize);
    }
}
