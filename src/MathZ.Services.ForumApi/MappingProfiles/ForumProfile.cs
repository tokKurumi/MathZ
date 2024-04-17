namespace MathZ.Services.IdentityApi.MappingProfiles;

using AutoMapper;
using MathZ.Services.ForumApi.Entities;
using MathZ.Services.ForumApi.Features.Commands.CreateDislike;
using MathZ.Services.ForumApi.Features.Commands.CreateLike;
using MathZ.Services.ForumApi.Features.Commands.CreateMessage;
using MathZ.Services.ForumApi.Models.Dtos;

public class ForumProfile : Profile
{
    public ForumProfile()
    {
        CreateMap<CreateMessageCommand, MessageDto>()
            .ForMember(dest => dest.ParentMessageId, opt => opt.MapFrom(src => src.ParentMessageId))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

        CreateMap<MessageDto, Message>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ParentMessageId, opt => opt.MapFrom(src => src.ParentMessageId))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ReverseMap();

        CreateMap<CreateLikeCommand, MessageLikeDto>()
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<MessageLikeDto, MessageLike>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ReverseMap();

        CreateMap<CreateDislikeCommand, MessageDislikeDto>()
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<MessageDislikeDto, MessageDislike>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ReverseMap();
    }
}
