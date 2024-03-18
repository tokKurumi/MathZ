namespace MathZ.Services.EmailApi.Services.IServices;

using MathZ.Services.EmailApi.Models;

public interface IEmailSenderService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The collection of email addresses to send the email to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmailAsync(IEnumerable<MailAddress> to, string subject, string body, CancellationToken cancellationToken = default);
}
