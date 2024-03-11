namespace MathZ.Services.EmailApi.Entities;

public class MailingEmail
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string MailingId { get; set; } = string.Empty;

    public Mailing? Mailing { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
