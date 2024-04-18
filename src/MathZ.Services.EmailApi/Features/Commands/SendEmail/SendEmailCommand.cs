namespace MathZ.Services.EmailApi.Features.Commands.SendEmail;

using System.ComponentModel;
using MathZ.Services.EmailApi.Models;
using MediatR;

[DisplayName("SendEmailRequest")]
public record SendEmailCommand(
    IEnumerable<MailAddress> To,
    string Subject,
    string Body)
    : IRequest;
