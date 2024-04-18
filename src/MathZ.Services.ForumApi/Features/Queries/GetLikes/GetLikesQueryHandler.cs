namespace MathZ.Services.ForumApi.Features.Queries.GetLikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetLikesQueryHandler(
    IForumService forumService)
    : IRequestHandler<PaginationQuery<MessageLikeDto>, PagedList<MessageLikeDto>>
{
    private readonly IForumService _forumService = forumService;

    public async Task<PagedList<MessageLikeDto>> Handle(PaginationQuery<MessageLikeDto> request, CancellationToken cancellationToken)
    {
        return await PagedList<MessageLikeDto>.CreateAsync(_forumService.GetLikes(), request, cancellationToken);
    }
}
