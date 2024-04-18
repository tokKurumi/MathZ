namespace MathZ.Services.ForumApi.Features.Queries.GetDislikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetDislikesQueryHandler(
    IForumService forumService)
    : IRequestHandler<GetDislikesQuery, PagedList<MessageDislikeDto>>
{
    private readonly IForumService _forumService = forumService;

    public Task<PagedList<MessageDislikeDto>> Handle(GetDislikesQuery request, CancellationToken cancellationToken)
    {
        return PagedList<MessageDislikeDto>.CreateAsync(_forumService.GetDislikes(request.MessageId), request, cancellationToken);
    }
}
