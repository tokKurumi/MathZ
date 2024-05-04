namespace MathZ.Services.EmailApi.Features.Queries.GetMailingSubscribersById;

using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetMailingSubscribersByIdQueryHandler(
    ISubscriptionService subscriptionService)
    : IRequestHandler<GetMailingSubscribersByIdQuery, PagedList<string>>
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public Task<PagedList<string>> Handle(GetMailingSubscribersByIdQuery request, CancellationToken cancellationToken)
    {
        return PagedList<string>.CreateAsync(_subscriptionService.GetMailingSubscribersById(request.MailingId), request, cancellationToken);
    }
}
