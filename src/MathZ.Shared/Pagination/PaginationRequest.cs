namespace MathZ.Shared.Pagination;

public class PaginationRequest
{
    public PaginationRequest()
    {
        PageNumber = 1;
        PageSize = 25;
    }

    public PaginationRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 50 ? 50 : pageSize;
    }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}
