namespace MathZ.Services.EmailApi.Features.Queries.GetMailingByTopic;

using FluentResults;
using MathZ.Services.EmailApi.Models.Dtos;
using MediatR;

public record GetMailingByTopicQuery(
    string Topic)
    : IRequest<Result<MailingDto>>;
