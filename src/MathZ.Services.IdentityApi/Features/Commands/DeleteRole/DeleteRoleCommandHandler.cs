namespace MathZ.Services.IdentityApi.Features.Commands.DeleteRole;

using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class DeleteRoleCommandHandler(
    IUserRolesService userRolesService)
    : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IUserRolesService _userRolesService = userRolesService;

    public Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _userRolesService.DeleteRoleAsync(request.Role);
    }
}
