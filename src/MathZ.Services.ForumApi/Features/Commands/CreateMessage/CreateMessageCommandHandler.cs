namespace MathZ.Services.ForumApi.Features.Commands.CreateMessage;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using MediatR;

public class CreateMessageCommandHandler(
    IMapper mapper,
    IForumService forumService)
    : IRequestHandler<CreateMessageCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IForumService _forumService = forumService;

    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<MessageDto>(request);
        await _forumService.SendMessageAsync(message, cancellationToken);
    }
}
