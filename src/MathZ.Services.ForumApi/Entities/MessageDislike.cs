namespace MathZ.Services.ForumApi.Entities;

public class MessageDislike
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string MessageId { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public Message? Message { get; set; }
}
