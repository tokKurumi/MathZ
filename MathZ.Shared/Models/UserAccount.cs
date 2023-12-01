namespace MathZ.Shared.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserAccount : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = string.Empty;

        [ProtectedPersonalData]
        public string LastName { get; set; } = string.Empty;
    }
}