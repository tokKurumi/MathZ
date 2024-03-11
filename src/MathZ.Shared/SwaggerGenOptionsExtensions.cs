namespace MathZ.Shared;

using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerGenOptionsExtensions
{
    public static SwaggerGenOptions UseDtoConversion(this SwaggerGenOptions options)
    {
        options.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName ?? x.Name);

        return options;
    }

    public static SwaggerGenOptions UseOAuth2(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        return options;
    }

    public static SwaggerGenOptions UseDocumentation(this SwaggerGenOptions options)
    {
        var documentationFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var documentationFilePath = Path.Combine(AppContext.BaseDirectory, documentationFile);
        if (File.Exists(documentationFilePath))
        {
            options.IncludeXmlComments(documentationFilePath);
        }

        return options;
    }
}
