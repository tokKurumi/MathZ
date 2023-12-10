namespace MathZ.Services.UserAPI.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using MathZ.Services.UserAPI.Exceptions;
    using MathZ.Services.UserAPI.Models;
    using MathZ.Services.UserAPI.Models.Dto;
    using MathZ.Services.UserAPI.Services.IServices;
    using MathZ.Shared.Exceptions;
    using MathZ.Shared.Models;
    using MathZ.Shared.Models.Dto;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.JsonPatch;

    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _userManager;

        public UserAccountService(
            IMapper mapper,
            UserManager<UserAccount> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserAccountDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotExistException();

            var userDto = _mapper.Map<UserAccountDto>(user);
            userDto.Roles = await _userManager.GetRolesAsync(user);

            return userDto;
        }

        public async Task<UserAccountDto> PatchProfileAsync(string userId, JsonPatchDocument<UserAccountPatchProfileModels> patchRequest)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotExistException();

            var existingProfile = _mapper.Map<UserAccountPatchProfileModels>(user);

            if (patchRequest is null)
            {
                throw new InvalidPatchDocumentException();
            }

            patchRequest.ApplyTo(existingProfile);

            _mapper.Map(existingProfile, user);

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new UpdateProfileException()
                {
                    Errors = updateResult.Errors,
                };
            }

            return await GetUserByIdAsync(userId);
        }

        public async Task UpdatePasswordAsync(string userId, UserAccountUpdatePasswordRequestDto updatePasswordRequest)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotExistException();

            var result = await _userManager.ChangePasswordAsync(user, updatePasswordRequest.Password, updatePasswordRequest.NewPassword);

            if (!result.Succeeded)
            {
                throw new UpdateProfileException()
                {
                    Errors = result.Errors,
                };
            }
        }
    }
}