namespace MathZ.Services.IdentityApi.Features.Commands.DeleteUser;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class DeleteUserCommandHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return _userAccountService.DeleteUserProfileByIdAsync(request.Id);
    }
}
