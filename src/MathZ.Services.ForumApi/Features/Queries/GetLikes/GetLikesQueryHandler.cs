namespace MathZ.Services.ForumApi.Features.Queries.GetLikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetLikesQueryHandler(
    IForumService forumService)
    : IRequestHandler<GetLikesQuery, PagedList<MessageLikeDto>>
{
    private readonly IForumService _forumService = forumService;

    public Task<PagedList<MessageLikeDto>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
    {
        return PagedList<MessageLikeDto>.CreateAsync(_forumService.GetLikes(request.MessageId), request, cancellationToken);
    }
}
