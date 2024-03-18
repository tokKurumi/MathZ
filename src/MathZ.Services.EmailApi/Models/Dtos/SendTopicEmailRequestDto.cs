namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("SendTopicEmailRequest")]
public record SendTopicEmailRequestDto(
    string Subject,
    string Body);
