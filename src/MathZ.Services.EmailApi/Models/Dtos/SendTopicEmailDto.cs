namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("SendTopicEmail")]
public record SendTopicEmailDto(
    string Subject,
    string Body);
