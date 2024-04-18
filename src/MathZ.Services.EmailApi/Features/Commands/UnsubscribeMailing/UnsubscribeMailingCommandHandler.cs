namespace MathZ.Services.EmailApi.Features.Commands.UnsubscribeMailing;

using FluentResults;
using MathZ.Services.EmailApi.Features.Commands.UnsibscribeMailing;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class UnsubscribeMailingCommandHandler(
    ISubscriptionService subscriptionService)
    : IRequestHandler<UnsubscribeMailingCommand, Result>
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public Task<Result> Handle(UnsubscribeMailingCommand request, CancellationToken cancellationToken)
    {
        return _subscriptionService.UnsubscribeMailingByIdAsync(request.MailingId, request.Email, cancellationToken);
    }
}
