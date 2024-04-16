namespace MathZ.Services.IdentityApi.Features.Commands.CreateUser;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class CreateUserCommandHandler(
    IMapper mapper,
    IAuthService authService,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        var result = await _authService.RegisterAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _publishEndpoint.Publish(user, cancellationToken);
        }

        return result;
    }
}
