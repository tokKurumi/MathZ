namespace MathZ.Services.EmailApi.MappingProfiles;

using AutoMapper;
using MathZ.Services.EmailApi.Entities;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Models.Dtos;
using MimeKit;

public class EmailProfile : Profile
{
    public EmailProfile()
    {
        CreateMap<Mailing, MailingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        CreateMap<Mailing, MailingPatch>()
            .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<MailAddress, MailboxAddress>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}
