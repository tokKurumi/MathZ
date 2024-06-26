﻿namespace MathZ.Services.IdentityApi.Services;

using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRolesService(
    IMapper mapper,
    RoleManager<IdentityRole> roleManager)
    : IUserRolesService
{
    private readonly IMapper _mapper = mapper;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<Result> CreateRoleAsync(string role)
    {
        var createResult = await _roleManager.CreateAsync(new IdentityRole(role));

        return createResult.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(createResult.Errors.Select(e => e.Description)),
        };
    }

    public async Task<Result> DeleteRoleAsync(string role)
    {
        var deleteResult = await _roleManager.DeleteAsync(new IdentityRole(role));

        return deleteResult.Succeeded switch
        {
            true => Result.Ok(),
            _ => Result.Fail(deleteResult.Errors.Select(e => e.Description)),
        };
    }

    public IQueryable<string> GetRoles()
    {
        return _mapper.ProjectTo<string>(_roleManager.Roles.AsNoTracking().OrderBy(r => r.Id));
    }
}
