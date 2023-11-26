namespace MathZ.Services.AuthAPI.Services.IServices
{
    using AutoMapper;
    using MathZ.Services.AuthAPI.Data;
    using MathZ.Services.AuthAPI.Exceptions;
    using MathZ.Shared.Models;
    using MathZ.Shared.Models.Dto;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IMapper mapper, AppDbContext dbContext, UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<UserAccountLoginResponseDto> Login(UserAccountLoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAccountDto> Register(UserAccountRegistrationRequestDto registrationRequestDto)
        {
            var user = _mapper.Map<UserAccount>(registrationRequestDto);

            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password ?? string.Empty);
            if (!result.Succeeded)
            {
                throw new CreateUserException()
                {
                    Problems = result.Errors,
                };
            }

            var userToReturn = await _dbContext.Users.FirstAsync(user => user.UserName == registrationRequestDto.Login);

            return _mapper.Map<UserAccountDto>(userToReturn);
        }
    }
}