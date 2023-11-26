namespace MathZ.Shared.Models.Dto
{
    public class UserAccountLoginResponseDto
    {
        public UserAccountDto User { get; set; }

        public string Token { get; set; }
    }
}