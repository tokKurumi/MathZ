namespace MathZ.Services.ForumApi.Models.Dtos;

public class MessageDto(
    string id,
    string? parentMessageId,
    string authorId,
    string text)
{
    public MessageDto()
        : this(Guid.NewGuid().ToString(), null, string.Empty, string.Empty)
    {
    }

    public string Id { get; set; } = id;

    public string? ParentMessageId { get; set; } = parentMessageId;

    public string AuthorId { get; set; } = authorId;

    public string Text { get; set; } = text;

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
