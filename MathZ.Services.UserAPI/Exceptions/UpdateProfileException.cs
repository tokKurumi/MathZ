namespace MathZ.Services.UserAPI.Exceptions
{
    using Microsoft.AspNetCore.Identity;

    public class UpdateProfileException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; } = Enumerable.Empty<IdentityError>();
    }
}