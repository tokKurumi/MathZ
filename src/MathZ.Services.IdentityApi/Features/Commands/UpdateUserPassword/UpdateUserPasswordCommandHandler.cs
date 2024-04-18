namespace MathZ.Services.IdentityApi.Features.Commands.UpdateUserPassword;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class UpdateUserPasswordCommandHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<UpdateUserPasswordCommand, Result>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        return _userAccountService.UpdateUserPasswordByIdAsync(request.Id, request.CurrentPassword, request.NewPassword);
    }
}
