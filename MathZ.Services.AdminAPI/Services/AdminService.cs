namespace MathZ.Services.AdminAPI.Services;

using AutoMapper;
using MathZ.Services.AdminAPI.Services.IServices;
using MathZ.Shared.Data;
using MathZ.Shared.Exceptions;
using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class AdminService(
    UsersDbContext dbContext,
    UserManager<UserAccount> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper,
    HttpClient httpClient)
    : IAdminService
{
    private readonly IMapper _mapper = mapper;
    private readonly UsersDbContext _dbContext = dbContext;
    private readonly UserManager<UserAccount> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<UserAccountDto> AddUserRolesAsync(string userId, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var existingRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = roles.Except(existingRoles);

        await _userManager.AddToRolesAsync(user, rolesToAdd);

        return await GetUserByIdAsync(userId);
    }

    public async Task<UserAccountDto> DeleteUserRolesAsync(string userId, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

        await _userManager.RemoveFromRolesAsync(user, roles);

        return await GetUserByIdAsync(userId);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

        await _userManager.DeleteAsync(user);
    }

    public async Task<IEnumerable<UserAccountDto>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users
            .Select(userAccount => new UserAccountDto
            {
                Id = userAccount.Id,
                Login = userAccount.UserName ?? string.Empty,
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                Roles = _dbContext.UserRoles
                    .Where(userRole => userRole.UserId == userAccount.Id)
                    .Join(_dbContext.Roles, userRole => userRole.RoleId, role => role.Id, (userRole, role) => role.Name ?? string.Empty)
                    .ToList(),
            })
            .ToListAsync();

        return users;
    }

    public async Task<UserAccountDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

        var userResponse = _mapper.Map<UserAccountDto>(user);
        userResponse.Roles = await _userManager.GetRolesAsync(user);

        return userResponse;
    }

    public async Task<UserAccountDto> PatchProfileAsync(string userId, JsonPatchDocument<UserAccountPatchProfileModels> patchRequest)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

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

    public async Task<UserAccountDto> RegisterUserAsync(string authorizationToken, UserAccountRegistrationRequestDto registrationRequestDto)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", authorizationToken);

        var response = await _httpClient.PostAsJsonAsync(@"api/auth/register", registrationRequestDto, default);

        if (!response.IsSuccessStatusCode)
        {
            throw new CreateUserException()
            {
                Problems = response.Content.ReadFromJsonAsAsyncEnumerable<IdentityError>().ToBlockingEnumerable().OfType<IdentityError>(),
            };
        }

        return JsonConvert.DeserializeObject<UserAccountDto>(await response.Content.ReadAsStringAsync())
            ?? throw new JsonSerializationException($"Failed to deserialize server response.");
    }

    public async Task ChangeUserPasswordAsync(string userId, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotExistException();

        var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, resetPassToken, newPassword);

        if (!result.Succeeded)
        {
            throw new UpdateProfileException()
            {
                Errors = result.Errors,
            };
        }
    }
}