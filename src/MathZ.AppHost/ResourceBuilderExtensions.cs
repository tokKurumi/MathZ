namespace MathZ.AppHost;

using Aspire.Hosting.ApplicationModel;
using MathZ.Shared;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<ProjectResource> WithJwt(this IResourceBuilder<ProjectResource> resourceBuilder, string? secret, string? audience, string? issuer)
    {
        return resourceBuilder
            .WithEnvironment(JwtEnvConstants.JwtSecret, secret)
            .WithEnvironment(JwtEnvConstants.JwtAudience, audience)
            .WithEnvironment(JwtEnvConstants.JwtIssuer, issuer);
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
