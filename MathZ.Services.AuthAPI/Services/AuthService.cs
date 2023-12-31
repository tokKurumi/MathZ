﻿namespace MathZ.Services.AuthAPI.Services;

using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using AutoMapper;
using MathZ.Services.AuthAPI.Models.Dto;
using MathZ.Services.AuthAPI.Services.IServices;
using MathZ.Shared.Data;
using MathZ.Shared.Exceptions;
using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService(
    IMapper mapper,
    UsersDbContext dbContext,
    UserManager<UserAccount> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtGeneratorService jwtGeneratorService)
    : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly UsersDbContext _dbContext = dbContext;
    private readonly UserManager<UserAccount> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IJwtGeneratorService _jwtGeneratorService = jwtGeneratorService;

    public async Task<IEnumerable<string>> AsignRoles(UserAccountAssignRolesRequestDto assignRolesRequestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.UserName == assignRolesRequestDto.Login)
            ?? throw new UserNotExistException();

        foreach (var role in assignRolesRequestDto.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var existingRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = assignRolesRequestDto.Roles.Except(existingRoles);

        await _userManager.AddToRolesAsync(user, rolesToAdd);

        return existingRoles.Concat(rolesToAdd);
    }

    public async Task<UserAccountLoginResponseDto> Login(UserAccountLoginRequestDto loginRequestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.UserName == loginRequestDto.Login);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
        {
            throw new AuthenticationException("Invalid username or password.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtGeneratorService.GenerateToken(user, roles);

        return new UserAccountLoginResponseDto()
        {
            Token = token,
        };
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

        var userRoles = await AsignRoles(_mapper.Map<UserAccountAssignRolesRequestDto>(registrationRequestDto));

        var returnValue = _mapper.Map<UserAccountDto>(await _dbContext.Users.FirstAsync(user => user.UserName == registrationRequestDto.Login));
        returnValue.Roles = userRoles;

        return returnValue;
    }
}