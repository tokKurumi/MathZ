namespace MathZ.Services.StatisticsAPI;

using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using MathZ.Services.ServiceDefaults;
using MathZ.Services.StatisticsAPI.Services;
using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IAdvancedAnalysisService, AdvancedAnalysisService>();
        builder.Services.AddScoped<IBasicAnalysisService, BasicAnalysisService>();
        builder.Services.AddScoped<IEstimatorService, EstimatorService>();
        builder.Services.AddScoped<IOthersAnalysisService, OthersAnalysisService>();
        builder.Services.AddScoped<IPopulationAnalysisService, PopulationAnalysisService>();
        builder.Services.AddScoped<IQuantileService, QuantileService>();

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "StatisticsAPI",
                Description = "An ASP.NET Core Web API for working with math statistics",
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