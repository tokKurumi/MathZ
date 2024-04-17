namespace MathZ.Services.ForumApi.Models.Dtos;

public class MessageDislikeDto(
    string id,
    string messageId,
    string userId)
{
    public MessageDislikeDto()
        : this(Guid.NewGuid().ToString(), string.Empty, string.Empty)
    {
    }

    public string Id { get; } = id;

    public string MessageId { get; } = messageId;

    public string UserId { get; } = userId;

    public DateTime Created { get; } = DateTime.UtcNow;
}
