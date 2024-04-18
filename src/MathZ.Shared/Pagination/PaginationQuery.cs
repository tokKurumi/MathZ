namespace MathZ.Shared.Pagination;

using System.ComponentModel;
using MediatR;

[DisplayName("Pagination")]
public class PaginationQuery<T>
    : IRequest<PagedList<T>>
{
    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 50;
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 100 ? 100 : pageSize;
    }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}
