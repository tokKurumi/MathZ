namespace MathZ.Shared.Models;

public record Email(
    IEnumerable<MailBoxAddress> From,
    IEnumerable<MailBoxAddress> To,
    string Subject,
    string Body);