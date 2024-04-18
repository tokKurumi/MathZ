namespace MathZ.Services.IdentityApi.Features.Queries.GetUserById;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class GetUserByIdQueryHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<GetUserByIdQuery, Result<ResponseUserDto>>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result<ResponseUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return _userAccountService.GetUserByIdAsync(request.Id);
    }
}
