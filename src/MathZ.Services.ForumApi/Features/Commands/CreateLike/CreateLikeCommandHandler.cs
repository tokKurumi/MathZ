namespace MathZ.Services.ForumApi.Features.Commands.CreateLike;

using AutoMapper;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MediatR;

public class CreateLikeCommandHandler(
    IMapper mapper,
    IForumService forumService)
    : IRequestHandler<CreateLikeCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IForumService _forumService = forumService;

    public async Task Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var like = _mapper.Map<MessageLikeDto>(request);
        await _forumService.LikeMessageAsync(like, cancellationToken);
    }
}
