namespace MathZ.Services.IdentityApi.Models;

public record DefaultUser(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    IEnumerable<string> Roles);
