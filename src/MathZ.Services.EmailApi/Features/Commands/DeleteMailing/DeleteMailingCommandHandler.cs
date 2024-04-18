namespace MathZ.Services.EmailApi.Features.Commands.DeleteMailing;

using FluentResults;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class DeleteMailingCommandHandler(
    IMailingService mailingService)
    : IRequestHandler<DeleteMailingCommand, Result>
{
    private readonly IMailingService _mailingService = mailingService;

    public Task<Result> Handle(DeleteMailingCommand request, CancellationToken cancellationToken)
    {
        return _mailingService.DeleteMailingByIdAsync(request.Id, cancellationToken);
    }
}
