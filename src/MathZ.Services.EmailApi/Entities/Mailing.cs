namespace MathZ.Services.EmailApi.Entities;

public class Mailing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Topic { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public List<MailingSubscriber> Subscribers { get; set; } = [];

    public List<MailingEmail> Emails { get; set; } = [];
}
