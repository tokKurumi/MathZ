namespace MathZ.Services.EmailApi.Features.Commands.SubscribeMailingById;

using FluentResults;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class SubscribeMailingByIdCommandHandler(
    ISubscriptionService subscriptionService)
    : IRequestHandler<SubscribeMailingByIdCommand, Result>
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public Task<Result> Handle(SubscribeMailingByIdCommand request, CancellationToken cancellationToken)
    {
        return _subscriptionService.SubscribeMailingByIdAsync(request.MailingId, request.Email, cancellationToken);
    }
}
