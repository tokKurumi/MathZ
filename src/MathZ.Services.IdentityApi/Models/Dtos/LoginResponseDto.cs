namespace MathZ.Services.IdentityApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("Token")]
public record LoginResponseDto(
    string Token);
