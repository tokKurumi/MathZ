namespace MathZ.Services.EmailApi.Features.Queries.GetMailingById;

using FluentResults;
using MathZ.Services.EmailApi.Models.Dtos;
using MediatR;

public record GetMailingByIdQuery(
    string Id)
    : IRequest<Result<MailingDto>>;
