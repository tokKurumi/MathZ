namespace MathZ.Services.EmailApi.Features.Queries.GetMailingByTopic;

using FluentResults;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class GetMailingByTopicQueryHandler(
    IMailingService mailingService)
    : IRequestHandler<GetMailingByTopicQuery, Result<MailingDto>>
{
    private readonly IMailingService _mailingService = mailingService;

    public async Task<Result<MailingDto>> Handle(GetMailingByTopicQuery request, CancellationToken cancellationToken)
    {
        return await _mailingService.GetMailingByTopicAsync(request.Topic, cancellationToken);
    }
}
