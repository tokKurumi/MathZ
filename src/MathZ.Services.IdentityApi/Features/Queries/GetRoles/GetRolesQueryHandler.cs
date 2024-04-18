namespace MathZ.Services.IdentityApi.Features.Queries.GetRoles;

using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetRolesQueryHandler(
    IUserRolesService userRolesService)
    : IRequestHandler<PaginationQuery<string>, PagedList<string>>
{
    private readonly IUserRolesService _userRolesService = userRolesService;

    public async Task<PagedList<string>> Handle(PaginationQuery<string> request, CancellationToken cancellationToken)
    {
        return await PagedList<string>.CreateAsync(_userRolesService.GetRoles(), request, cancellationToken);
    }
}
