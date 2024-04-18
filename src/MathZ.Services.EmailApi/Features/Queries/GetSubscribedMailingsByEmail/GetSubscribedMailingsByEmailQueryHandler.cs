namespace MathZ.Services.EmailApi.Features.Queries.GetSubscribedMailingsByEmail;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetSubscribedMailingsByEmailQueryHandler(
    ISubscriptionService subscriptionService)
    : IRequestHandler<GetSubscribedMailingsByEmailQuery, PagedList<MailingDto>>
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public Task<PagedList<MailingDto>> Handle(GetSubscribedMailingsByEmailQuery request, CancellationToken cancellationToken)
    {
        return PagedList<MailingDto>.CreateAsync(_subscriptionService.GetSubscribedMailingsByEmail(request.Email), request, cancellationToken);
    }
}
