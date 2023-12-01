namespace MathZ.Services.AuthAPI.Services.IServices
{
    using MathZ.Services.AuthAPI.Models.Dto;
    using MathZ.Shared.Models.Dto;

    public interface IAuthService
    {
        Task<UserAccountDto> Register(UserAccountRegistrationRequestDto registrationRequestDto);

        Task<UserAccountLoginResponseDto> Login(UserAccountLoginRequestDto loginRequestDto);

        Task<IEnumerable<string>> AsignRoles(UserAccountAssignRolesRequestDto assignRolesRequestDto);
    }
}