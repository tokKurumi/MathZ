namespace MathZ.Services.AuthAPI.Models.Dto;

using System.ComponentModel;

[DisplayName("LoginRequest")]
public class UserAccountLoginRequestDto
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}