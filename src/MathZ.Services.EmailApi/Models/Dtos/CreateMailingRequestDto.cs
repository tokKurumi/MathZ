namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("CreateMailingRequest")]
public record CreateMailingRequestDto(
    string Topic,
    string? Description);
