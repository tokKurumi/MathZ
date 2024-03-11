namespace MathZ.Services.IdentityApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("RegistrationRequest")]
public record RegistrationRequestDto(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName);
