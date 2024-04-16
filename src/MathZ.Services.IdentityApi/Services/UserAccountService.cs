namespace MathZ.Services.IdentityApi.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

public class UserAccountService(
    UserManager<User> userManager,
    IMapper mapper)
    : IUserAccountService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> AddUserRolesByIdAsync(string id, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var result = await _userManager.AddToRolesAsync(user, roles);
        return result.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(result.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result> DeleteUserProfileByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(result.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result<ResponseUserDto>> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user switch
        {
            not null => _mapper.Map<ResponseUserDto>(user),
            _ => Result.Fail("There is no user with this Email."),
        };
    }

    public async Task<Result<ResponseUserDto>> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return user switch
        {
            not null => _mapper.Map<ResponseUserDto>(user),
            _ => Result.Fail("There is no user with this Id."),
        };
    }

    public async Task<Result<ResponseUserDto>> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user switch
        {
            not null => _mapper.Map<ResponseUserDto>(user),
            _ => Result.Fail("There is no user with this UserName."),
        };
    }

    public async Task<IEnumerable<string>> GetUserRolesByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return user switch
        {
            not null => await _userManager.GetRolesAsync(user),
            _ => [],
        };
    }

    public async Task<IEnumerable<ResponseUserDto>> GetUsersAsync(int skip, int count, CancellationToken cancellationToken = default)
    {
        return await _mapper.ProjectTo<ResponseUserDto>(
            _userManager.Users
            .AsNoTracking()
            .Skip(skip)
            .Take(count))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> GetUsersCountAsync(CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<ResponseUserDto>> GetUsersInRoleAsync(string role, int skip, int count)
    {
        return _mapper.Map<IEnumerable<ResponseUserDto>>((await _userManager.GetUsersInRoleAsync(role))
            .Skip(skip)
            .Take(count));
    }

    public async Task<Result<ResponseUserDto>> PatchUserAccountByIdAsync(string id, JsonPatchDocument<UserPatchProfile> patch)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var patched = _mapper.Map<UserPatchProfile>(user);
        patch.ApplyTo(patched);

        user.FirstName = patched.FirstName;
        user.LastName = patched.LastName;

        var updateRupdateResult = await _userManager.UpdateAsync(user);
        return updateRupdateResult.Succeeded switch
        {
            true => _mapper.Map<ResponseUserDto>(user),
            _ => Result.Fail(updateRupdateResult.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result> RemoveUserRolesByIdAsync(string id, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);
        return removeResult.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(removeResult.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result> UpdateUserPasswordByIdAsync(string id, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var updatePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return updatePasswordResult.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(updatePasswordResult.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result> UpdateUserPasswordByIdAsync(string id, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return Result.Fail("The is no user with this Id.");
        }

        var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var updatePasswordResult = await _userManager.ResetPasswordAsync(user, resetPassToken, newPassword);
        return updatePasswordResult.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(updatePasswordResult.Errors.Select(e => e.Description)),
        };
    }
}
