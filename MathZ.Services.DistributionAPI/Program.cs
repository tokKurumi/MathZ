namespace MathZ.Services.DistributionAPI;

using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using MathZ.Services.DistributionAPI.Services;
using MathZ.Services.DistributionAPI.Services.IServices;
using MathZ.Services.ServiceDefaults;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IDiscreteDistributionService, DiscreteDistributionService>();
        builder.Services.AddScoped<IContinuousDistributionService, ContinuousDistributionService>();
        builder.Services.AddScoped<IAnalysisService, AnalysisService>();

        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "DistributionAPI",
                Description = "An ASP.NET Core Web API for working with probability and distributions",
                Contact = new OpenApiContact
                {
                    Name = "Yudashkin Oleg",
                    Url = new Uri(@"https://github.com/tokKurumi"),
                    Email = @"tokkurumi901@gmail.com",
                },
            });

            options.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName ?? x.Name);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            options.EnableAnnotations();

            options.OperationFilter<SecurityRequirementsOperationFilter>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        builder.AddServiceDefaults();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.EnablePersistAuthorization();
            config.DisplayRequestDuration();
            config.EnableFilter();
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}