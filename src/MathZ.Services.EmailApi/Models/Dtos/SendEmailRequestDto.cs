namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("SendEmailRequest")]
public record SendEmailRequestDto(
    IEnumerable<MailAddress> To,
    string Subject,
    string Body);
