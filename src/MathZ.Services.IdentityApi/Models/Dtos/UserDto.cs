namespace MathZ.Services.IdentityApi.Models.Dtos;

public record UserDto(
    string Id,
    string Email,
    bool EmailConfirmed,
    string UserName,
    string Password,
    string FirstName,
    string LastName);
