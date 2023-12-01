namespace MathZ.Shared.Models.Dto
{
    public class UserAccountRegistrationRequestDto
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}