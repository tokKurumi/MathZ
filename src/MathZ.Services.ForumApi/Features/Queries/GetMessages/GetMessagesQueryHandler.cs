namespace MathZ.Services.ForumApi.Features.Queries.GetMessages;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Pagination;
using MathZ.Services.ForumApi.Services.IServices;
using MediatR;

public class GetMessagesQueryHandler(
    IForumService forumService)
    : IRequestHandler<PaginationQuery<MessageDto>, PagedList<MessageDto>>
{
    private readonly IForumService _forumService = forumService;

    public async Task<PagedList<MessageDto>> Handle(PaginationQuery<MessageDto> request, CancellationToken cancellationToken)
    {
        return await PagedList<MessageDto>.CreateAsync(_forumService.GetMessages(), request);
    }
}
