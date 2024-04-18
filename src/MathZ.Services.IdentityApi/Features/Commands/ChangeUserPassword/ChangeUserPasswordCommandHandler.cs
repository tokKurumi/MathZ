namespace MathZ.Services.IdentityApi.Features.Commands.ChangeUserPassword;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class ChangeUserPasswordCommandHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<ChangeUserPasswordCommand, Result>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        return _userAccountService.ChangeUserPasswordAsync(request.Id, request.NewPassword);
    }
}
