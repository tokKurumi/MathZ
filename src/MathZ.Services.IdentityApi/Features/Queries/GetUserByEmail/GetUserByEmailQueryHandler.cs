namespace MathZ.Services.IdentityApi.Features.Queries.GetUserByEmail;

using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class GetUserByEmailQueryHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<GetUserByEmailQuery, Result<ResponseUserDto>>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result<ResponseUserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return _userAccountService.GetUserByEmailAsync(request.Email);
    }
}
