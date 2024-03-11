namespace MathZ.Services.EmailApi.Models;

public record Email(
    IEnumerable<MailAddress> From,
    IEnumerable<MailAddress> To,
    string Subject,
    string Body);
