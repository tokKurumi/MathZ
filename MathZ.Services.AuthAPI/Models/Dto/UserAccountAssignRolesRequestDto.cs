namespace MathZ.Services.AuthAPI.Models.Dto
{
    public class UserAccountAssignRolesRequestDto
    {
        public string Login { get; set; } = string.Empty;

        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}