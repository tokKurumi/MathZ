namespace MathZ.Services.IdentityApi.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; } = string.Empty;

    [ProtectedPersonalData]
    public string? LastName { get; set; } = string.Empty;
}
