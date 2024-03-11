namespace MathZ.Services.EmailApi.Services.IServices;

using MathZ.Services.EmailApi.Models;

public interface IEmailSenderService
{
    Task SendEmailAsync(IEnumerable<MailAddress> to, string subject, string body, CancellationToken cancellationToken = default);
}
