namespace MathZ.Services.ForumApi.Features.Commands.CreateDislike;

using AutoMapper;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MediatR;

public class CreateDislikeCommandHandler(
    IMapper mapper,
    IForumService forumService)
    : IRequestHandler<CreateDislikeCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IForumService _forumService = forumService;

    public async Task Handle(CreateDislikeCommand request, CancellationToken cancellationToken)
    {
        var dislike = _mapper.Map<MessageDislikeDto>(request);
        await _forumService.DislikeMessageAsync(dislike, cancellationToken);
    }
}
