namespace MathZ.Services.EmailApi.Services;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.EmailApi.Data;
using Microsoft.Extensions.Logging;

public class DatabaseInitializerService(
    IServiceProvider serviceProvider,
    ILogger<DatabaseInitializerService> logger)
    : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<DatabaseInitializerService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MailingDbContext>();

        await DatabaseEnsureCreatedAsync(dbContext, stoppingToken);
    }

    private async Task DatabaseEnsureCreatedAsync(MailingDbContext dbContext, CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        _logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }
}
