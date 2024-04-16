namespace MathZ.Shared.ServicesHelpers;

using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'V";
                config.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}
