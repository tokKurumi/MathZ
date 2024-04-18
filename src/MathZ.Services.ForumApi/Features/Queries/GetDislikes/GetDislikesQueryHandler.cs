namespace MathZ.Services.ForumApi.Features.Queries.GetDislikes;

using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetDislikesQueryHandler(
    IForumService forumService)
    : IRequestHandler<PaginationQuery<MessageDislikeDto>, PagedList<MessageDislikeDto>>
{
    private readonly IForumService _forumService = forumService;

    public async Task<PagedList<MessageDislikeDto>> Handle(PaginationQuery<MessageDislikeDto> request, CancellationToken cancellationToken)
    {
        return await PagedList<MessageDislikeDto>.CreateAsync(_forumService.GetDislikes(), request, cancellationToken);
    }
}
