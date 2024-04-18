namespace MathZ.Services.IdentityApi.Features.Queries.GetUserByUserName;

using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class GetUsersUserNameQueryHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<GetUserByUserNameQuery, Result<ResponseUserDto>>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result<ResponseUserDto>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        return _userAccountService.GetUserByUserNameAsync(request.UserName);
    }
}
