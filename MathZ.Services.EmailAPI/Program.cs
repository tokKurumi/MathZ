namespace MathZ.Services.EmailAPI;

using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using MailKit.Net.Smtp;
using MathZ.Services.EmailAPI.Services;
using MathZ.Services.EmailAPI.Services.IServices;
using MathZ.Services.ServiceDefaults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.Configure<SmtpConfig>(config =>
        {
            config.From = builder.Configuration.GetValue<string>("SMTP_FROM") ?? string.Empty;
            config.Host = builder.Configuration.GetValue<string>("SMTP_HOST") ?? string.Empty;
            config.Port = builder.Configuration.GetValue<int>("FROM_PORT");
            config.UserName = builder.Configuration.GetValue<string>("FROM_USERNAME") ?? string.Empty;
            config.Password = builder.Configuration.GetValue<string>("FROM_PASSWORD") ?? string.Empty;
        });
        builder.Services.AddScoped(serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IOptions<SmtpConfig>>().Value;

            var smtp = new SmtpClient();
            smtp.Connect(config.Host, config.Port, true);
            smtp.Authenticate(config.UserName, config.Password);

            return smtp;
        });

        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "EmailAPI",
                Description = "An ASP.NET Core Web API for sending emails",
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
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}