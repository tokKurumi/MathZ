namespace MathZ.Services.IdentityApi.Features.Commands.CreateRole;

using FluentResults;
using MathZ.Services.IdentityApi.Services.IServices;
using MediatR;

public class CreateRoleCommandHandler(
    IUserRolesService userRolesService)
    : IRequestHandler<CreateRoleCommand, Result>
{
    private readonly IUserRolesService _userRolesService = userRolesService;

    public Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return _userRolesService.CreateRoleAsync(request.Role);
    }
}
