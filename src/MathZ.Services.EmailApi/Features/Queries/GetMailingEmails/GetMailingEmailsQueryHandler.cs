namespace MathZ.Services.EmailApi.Features.Queries.GetMailingEmails;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetMailingEmailsQueryHandler(
    IMailingService mailingService)
    : IRequestHandler<PaginationQuery<MailingEmailDto>, PagedList<MailingEmailDto>>
{
    private readonly IMailingService _mailingService = mailingService;

    public async Task<PagedList<MailingEmailDto>> Handle(PaginationQuery<MailingEmailDto> request, CancellationToken cancellationToken)
    {
        return await PagedList<MailingEmailDto>.CreateAsync(_mailingService.GetMailingEmails(), request, cancellationToken);
    }
}
