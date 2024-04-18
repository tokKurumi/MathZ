namespace MathZ.Services.EmailApi.Features.Commands.CreateMailing;

using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class CreateMailingCommandHandler(
    IMailingService mailingService)
    : IRequestHandler<CreateMailingCommand>
{
    private readonly IMailingService _mailingService = mailingService;

    public Task Handle(CreateMailingCommand request, CancellationToken cancellationToken)
    {
        return _mailingService.CreateMailingAsync(request.Topic, request.Description, cancellationToken);
    }
}
