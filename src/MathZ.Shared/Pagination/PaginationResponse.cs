namespace MathZ.Shared.Pagination;

public record PaginationResponse<T>(
    int Total,
    int PageNumber,
    int PageSize,
    IEnumerable<T> Data);
