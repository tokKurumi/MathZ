namespace MathZ.Services.IdentityApi.Features.Queries.GetUsers;

using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Pagination;
using MediatR;

public class GetUsersQueryHandler(
    IUserAccountService userAccountService)
    : IRequestHandler<PaginationQuery<ResponseUserDto>, PagedList<ResponseUserDto>>
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    public async Task<PagedList<ResponseUserDto>> Handle(PaginationQuery<ResponseUserDto> request, CancellationToken cancellationToken)
    {
        return await PagedList<ResponseUserDto>.CreateAsync(_userAccountService.GetUsers(), request, cancellationToken);
    }
}
