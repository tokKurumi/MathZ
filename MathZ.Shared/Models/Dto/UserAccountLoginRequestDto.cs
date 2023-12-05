namespace MathZ.Shared.Models.Dto
{
    using System.ComponentModel;

    [DisplayName("LoginRequest")]
    public class UserAccountLoginRequestDto
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}