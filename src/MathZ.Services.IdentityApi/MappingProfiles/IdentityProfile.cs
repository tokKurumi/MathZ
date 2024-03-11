namespace MathZ.Services.IdentityApi.MappingProfiles;

using AutoMapper;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<DefaultUser, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpperInvariant()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<RegistrationRequestDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpperInvariant()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<User, ResponseUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<User, UserPatchProfile>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<IdentityRole, string>()
            .ConvertUsing(r => r.Name ?? string.Empty);
    }
}
