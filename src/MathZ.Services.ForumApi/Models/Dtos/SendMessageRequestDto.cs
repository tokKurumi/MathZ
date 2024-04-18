namespace MathZ.Services.ForumApi.Models.Dtos;

using System.ComponentModel;
using MediatR;

[DisplayName("SendMessageRequest")]
public record SendMessageRequestDto(
    string? ParentMessageId,
    string Text)
    : IRequest;
