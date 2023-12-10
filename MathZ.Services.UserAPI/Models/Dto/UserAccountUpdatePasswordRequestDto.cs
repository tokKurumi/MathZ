namespace MathZ.Services.UserAPI.Models.Dto
{
    using System.ComponentModel;

    [DisplayName("UpdatePasswordRequest")]
    public class UserAccountUpdatePasswordRequestDto
    {
        public string Password { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}