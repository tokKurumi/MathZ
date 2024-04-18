namespace MathZ.Services.IdentityApi.Features.Commands.PatchUser;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class PatchUserCommandHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<PatchUserCommand, Result<ResponseUserDto>>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result<ResponseUserDto>> Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        return _userAccountService.PatchUserAccountByIdAsync(request.Id, request.Patch);
    }
}
