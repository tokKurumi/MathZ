namespace MathZ.Services.ForumApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("DislikeMessageRequest")]
public record DislikeMessageRequestDto(
    string MessageId);
