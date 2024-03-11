namespace MathZ.Services.IdentityApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("UpdatePasswordRequest")]
public record UpdatePasswordRequestDto(
    string CurrentPassword,
    string NewPassword);
