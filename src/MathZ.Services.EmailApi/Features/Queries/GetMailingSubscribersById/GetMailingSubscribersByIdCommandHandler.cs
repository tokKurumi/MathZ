namespace MathZ.Services.EmailApi.Features.Queries.GetMailingSubscribersById;

using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetMailingSubscribersByIdCommandHandler(
    ISubscriptionService subscriptionService)
    : IRequestHandler<GetMailingSubscribersByIdCommand, PagedList<string>>
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public Task<PagedList<string>> Handle(GetMailingSubscribersByIdCommand request, CancellationToken cancellationToken)
    {
        return PagedList<string>.CreateAsync(_subscriptionService.GetMailingSubscribersById(request.MailingId), request, cancellationToken);
    }
}
