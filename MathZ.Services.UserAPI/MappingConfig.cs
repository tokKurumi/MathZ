namespace MathZ.Services.UserAPI;

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
                Email = converter.Email ?? string.Empty,
                UserName = converter.UserName ?? string.Empty,
                FirstName = converter.FirstName,
                LastName = converter.LastName,
            });

            config.CreateMap<UserAccountDto, UserAccount>()
                .ConvertUsing(converter => new UserAccount()
                {
                    Id = converter.Id,
                    UserName = converter.UserName,
                    NormalizedUserName = converter.UserName.ToUpper(),
                    FirstName = converter.FirstName,
                    LastName = converter.LastName,
                });

            config.CreateMap<UserAccount, UserAccountPatchProfileModels>()
                .ConstructUsing(converter => new UserAccountPatchProfileModels()
                {
                    FirstName = converter.FirstName,
                    LastName = converter.LastName,
                });

            config.CreateMap<UserAccountPatchProfileModels, UserAccount>()
                .ConstructUsing(converter => new UserAccount()
                {
                    FirstName = converter.FirstName,
                    LastName = converter.LastName,
                });
        });
    }
}