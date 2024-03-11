namespace MathZ.Services.IdentityApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("User")]
public record ResponseUserDto(
    string Id,
    string Email,
    string UserName,
    string FirstName,
    string LastName);
