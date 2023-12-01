namespace MathZ.Shared.Models.Dto
{
    public class UserAccountLoginResponseDto
    {
        public UserAccountDto User { get; set; } = new UserAccountDto();

        public string Token { get; set; } = string.Empty;
    }
}