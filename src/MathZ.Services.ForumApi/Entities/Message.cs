namespace MathZ.Services.ForumApi.Entities;

public class Message
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? ParentMessageId { get; set; }

    public string AuthorId { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public Message? ParentMessage { get; set; }

    public List<Message> Replies { get; set; } = [];

    public List<MessageLike> Likes { get; set; } = [];

    public List<MessageDislike> Dislikes { get; set; } = [];
}
