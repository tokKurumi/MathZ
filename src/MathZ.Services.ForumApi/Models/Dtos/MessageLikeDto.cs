namespace MathZ.Services.ForumApi.Models.Dtos;

public class MessageLikeDto(
    string id,
    string messageId,
    string userId)
{
    public MessageLikeDto()
        : this(Guid.NewGuid().ToString(), string.Empty, string.Empty)
    {
    }

    public string Id { get; set; } = id;

    public string MessageId { get; set; } = messageId;

    public string UserId { get; set; } = userId;

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
