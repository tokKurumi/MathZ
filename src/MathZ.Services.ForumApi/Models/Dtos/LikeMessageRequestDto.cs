namespace MathZ.Services.ForumApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("LikeMessageRequest")]
public record LikeMessageRequestDto(
    string MessageId);
