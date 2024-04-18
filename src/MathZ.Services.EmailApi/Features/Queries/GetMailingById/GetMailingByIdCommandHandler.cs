namespace MathZ.Services.EmailApi.Features.Queries.GetMailingById;

using FluentResults;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class GetMailingByIdCommandHandler(
    IMailingService mailingService)
    : IRequestHandler<GetMailingByIdQuery, Result<MailingDto>>
{
    private readonly IMailingService _mailingService = mailingService;

    public Task<Result<MailingDto>> Handle(GetMailingByIdQuery request, CancellationToken cancellationToken)
    {
        return _mailingService.GetMailingByIdAsync(request.Id, cancellationToken);
    }
}
