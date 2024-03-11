namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("Mailing")]
public record MailingDto(
    string Id,
    string Topic,
    string? Description,
    DateTime CreatedDate);
