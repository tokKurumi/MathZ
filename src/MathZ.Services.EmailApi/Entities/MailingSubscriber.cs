namespace MathZ.Services.EmailApi.Entities;

public class MailingSubscriber
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Email { get; set; } = string.Empty;

    public string MailingId { get; set; } = string.Empty;

    public Mailing? Mailing { get; set; }
}
