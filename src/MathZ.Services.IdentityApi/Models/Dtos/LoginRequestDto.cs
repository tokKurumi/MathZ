namespace MathZ.Services.IdentityApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("LoginRequest")]
public record LoginRequestDto(
    string UserName,
    string Password);
