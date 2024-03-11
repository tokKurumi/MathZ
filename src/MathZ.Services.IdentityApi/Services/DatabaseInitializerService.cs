namespace MathZ.Services.IdentityApi.Services;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MathZ.Services.IdentityApi.Data;
using MathZ.Services.IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class DatabaseInitializerService(
    IConfiguration configuration,
    IMapper mapper,
    IServiceProvider serviceProvider,
    ILogger<DatabaseInitializerService> logger)
    : BackgroundService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IMapper _mapper = mapper;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<DatabaseInitializerService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserIdentityDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await DatabaseEnsureCreatedAsync(dbContext, stoppingToken);
        await CreateDefaultRolesAsync(roleManager);
        await CreateDefaultUsersAsync(userManager);
    }

    private async Task DatabaseEnsureCreatedAsync(UserIdentityDbContext dbContext, CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        _logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }

    private async Task CreateDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var sw = Stopwatch.StartNew();

        var defaultRoles = _configuration
            .GetRequiredSection("DafaultRoles")
            .Get<IEnumerable<string>>()
            ?? [];

        foreach (var role in defaultRoles)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }

        _logger.LogInformation("All default roles were successfully created after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }

    private async Task CreateDefaultUsersAsync(UserManager<User> userManager)
    {
        var sw = Stopwatch.StartNew();

        var defaultUsers = _configuration
            .GetRequiredSection("DefaultUsers")
            .Get<IEnumerable<DefaultUser>>()
            ?? [];

        foreach (var defaultUser in defaultUsers)
        {
            if ((await userManager.FindByNameAsync(defaultUser.UserName)) is not null)
            {
                _logger.LogInformation("User {UserName} already exists. Skipping creation.", defaultUser.UserName);
                continue;
            }

            var user = _mapper.Map<User>(defaultUser);

            var userCreationResult = await userManager.CreateAsync(user, defaultUser.Password);
            if (!userCreationResult.Succeeded)
            {
                _logger.LogError("Failed to create user {UserName}. Errors: {Errors}", defaultUser.UserName, userCreationResult.Errors);
            }

            if (defaultUser.Roles.Any())
            {
                var rolesAddingResult = await userManager.AddToRolesAsync(user, defaultUser.Roles);
                if (!rolesAddingResult.Succeeded)
                {
                    _logger.LogError("Failed to add roles to user {UserName}. Errors: {Errors}", defaultUser.UserName, rolesAddingResult.Errors);
                }
            }
        }

        _logger.LogInformation("All default users were successfully created after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }
}
