namespace MathZ.Services.ForumApi.Features.Queries.GetDislikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Shared.Pagination;

public class GetDislikesQuery
    : PaginationQuery<MessageDislikeDto>
{
    public GetDislikesQuery()
        : base()
    {
        MessageId = string.Empty;
    }

    public GetDislikesQuery(string messageId, PaginationQuery<MessageLikeDto> paginationQuery)
    {
        MessageId = messageId;

        PageNumber = paginationQuery.PageNumber;
        PageSize = paginationQuery.PageSize;
    }

    public string MessageId { get; set; }
}
