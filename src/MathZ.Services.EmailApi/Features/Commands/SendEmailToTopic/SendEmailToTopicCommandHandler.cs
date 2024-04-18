namespace MathZ.Services.EmailApi.Features.Commands.SendEmailToTopic;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class SendEmailToTopicCommandHandler(
    IMailingService mailingService,
    IEmailSenderService emailSenderService)
    : IRequestHandler<SendEmailToTopicCommand, Result>
{
    private readonly IMailingService _mailingService = mailingService;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;

    public async Task<Result> Handle(SendEmailToTopicCommand request, CancellationToken cancellationToken)
    {
        var emailsResult = await _mailingService.SendEmailByIdAsync(request.Id, request.Subject, request.Body, cancellationToken);
        if (!emailsResult.IsSuccess)
        {
            return Result.Fail(emailsResult.Errors);
        }

        var to = emailsResult.Value.Select(e => new MailAddress(string.Empty, e));
        await _emailSenderService.SendEmailAsync(to, request.Subject, request.Body, cancellationToken);

        return Result.Ok();
    }
}
