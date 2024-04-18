namespace MathZ.Services.ForumApi.Features.Queries.GetLikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Shared.Pagination;

public class GetLikesQuery
    : PaginationQuery<MessageLikeDto>
{
    public GetLikesQuery()
        : base()
    {
        MessageId = string.Empty;
    }

    public GetLikesQuery(string messageId, PaginationQuery<MessageLikeDto> paginationQuery)
    {
        MessageId = messageId;

        PageNumber = paginationQuery.PageNumber;
        PageSize = paginationQuery.PageSize;
    }

    public string MessageId { get; set; }
}
