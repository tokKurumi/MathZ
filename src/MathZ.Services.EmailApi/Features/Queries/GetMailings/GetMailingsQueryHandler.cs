namespace MathZ.Services.EmailApi.Features.Queries.GetMailings;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetMailingsQueryHandler(
    IMailingService mailingService)
    : IRequestHandler<PaginationQuery<MailingDto>, PagedList<MailingDto>>
{
    private readonly IMailingService _mailingService = mailingService;

    public Task<PagedList<MailingDto>> Handle(PaginationQuery<MailingDto> request, CancellationToken cancellationToken)
    {
        return PagedList<MailingDto>.CreateAsync(_mailingService.GetMailings(), request, cancellationToken);
    }
}
