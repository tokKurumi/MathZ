namespace MathZ.Services.EmailApi.Features.Commands.UpdateMailing;

using FluentResults;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class UpdateMailingCommandHandler(
    IMailingService mailingService)
    : IRequestHandler<UpdateMailingCommand, Result>
{
    private readonly IMailingService _mailingService = mailingService;

    public Task<Result> Handle(UpdateMailingCommand request, CancellationToken cancellationToken)
    {
        return _mailingService.UpdateMailingByIdAsync(request.Id, request.Patch, cancellationToken);
    }
}
