namespace MathZ.AppHost;

using Aspire.Hosting.ApplicationModel;
using MathZ.Shared.Jwt;
using MathZ.Shared.Smtp;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<ProjectResource> WithJwt(this IResourceBuilder<ProjectResource> resourceBuilder, string configSectionName)
    {
        var configuration = resourceBuilder.ApplicationBuilder.Configuration;

        var jwtSecret = configuration[$"{configSectionName}:Secret"];
        var jwtAudience = configuration[$"{configSectionName}:Audience"];
        var jwtIssuer = configuration[$"{configSectionName}:Issuer"];

        return resourceBuilder.WithJwt(jwtSecret, jwtAudience, jwtIssuer);
    }

    public static IResourceBuilder<ProjectResource> WithJwt(this IResourceBuilder<ProjectResource> resourceBuilder, string? secret, string? audience, string? issuer)
    {
        return resourceBuilder
            .WithEnvironment(JwtEnvConstants.JwtSecret, secret)
            .WithEnvironment(JwtEnvConstants.JwtAudience, audience)
            .WithEnvironment(JwtEnvConstants.JwtIssuer, issuer);
    }

    public static IResourceBuilder<ProjectResource> WithSmtp(this IResourceBuilder<ProjectResource> resourceBuilder, string configSectionName)
    {
        var configuration = resourceBuilder.ApplicationBuilder.Configuration;

        var smtpFrom = configuration[$"{configSectionName}:From"];
        var smtpHost = configuration[$"{configSectionName}:Host"];
        var smtpPort = int.Parse(configuration[$"{configSectionName}:Port"] ?? string.Empty);
        var smtpUserName = configuration[$"{configSectionName}:UserName"];
        var smtpPassword = configuration[$"{configSectionName}:Password"];

        return resourceBuilder.WithSmtp(smtpFrom, smtpHost, smtpPort, smtpUserName, smtpPassword);
    }

    public static IResourceBuilder<ProjectResource> WithSmtp(this IResourceBuilder<ProjectResource> resourceBuilder, string? from, string? host, int port, string? userName, string? password)
    {
        return resourceBuilder
            .WithEnvironment(SmtpEnvConstants.From, from)
            .WithEnvironment(SmtpEnvConstants.Host, host)
            .WithEnvironment(SmtpEnvConstants.Port, port.ToString())
            .WithEnvironment(SmtpEnvConstants.UserName, userName)
            .WithEnvironment(SmtpEnvConstants.Password, password);
    }
}
