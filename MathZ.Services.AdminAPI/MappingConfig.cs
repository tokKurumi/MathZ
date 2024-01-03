namespace MathZ.Services.AdminAPI;

using AutoMapper;
using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<UserAccount, UserAccountDto>()
                .ConvertUsing(converter => new UserAccountDto
                {
                    Id = converter.Id,
                    Login = converter.UserName ?? string.Empty,
                    FirstName = converter.FirstName,
                    LastName = converter.LastName,
                });
        });
    }
}