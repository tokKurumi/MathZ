namespace MathZ.Services.IdentityApi.Features.Commands.GetToken;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Models;
using MediatR;

public class GetTokenCommandHandler(
    IMapper mapper,
    IAuthService authService)
    : IRequestHandler<GetTokenCommand, Result<JwtToken>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;

    public async Task<Result<JwtToken>> Handle(GetTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        return await _authService.LoginAsync(user, request.Password);
    }
}
