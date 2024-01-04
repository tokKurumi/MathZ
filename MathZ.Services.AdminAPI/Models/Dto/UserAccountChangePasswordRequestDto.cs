namespace MathZ.Services.AdminAPI.Models.Dto;

using System.ComponentModel;

[DisplayName("ChangeUserPasswordRequest")]
public class UserAccountChangePasswordRequestDto
{
    public string NewPassword { get; set; } = string.Empty;
}