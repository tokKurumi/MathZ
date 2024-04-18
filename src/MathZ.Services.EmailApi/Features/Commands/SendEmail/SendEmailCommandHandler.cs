namespace MathZ.Services.EmailApi.Features.Commands.SendEmail;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.EmailApi.Services.IServices;
using MediatR;

public class SendEmailCommandHandler(
    IEmailSenderService emailSenderService)
    : IRequestHandler<SendEmailCommand>
{
    private readonly IEmailSenderService _emailSenderService = emailSenderService;

    public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        return _emailSenderService.SendEmailAsync(request.To, request.Subject, request.Body, cancellationToken);
    }
}
