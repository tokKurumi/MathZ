namespace MathZ.Shared.Models.Dto
{
    public class UserAccountDto
    {
        public string Id { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}