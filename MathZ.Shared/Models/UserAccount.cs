namespace MathZ.Shared.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}