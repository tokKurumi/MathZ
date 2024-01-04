namespace MathZ.Services.AuthAPI;

using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using MathZ.Services.AuthAPI.Services;
using MathZ.Services.AuthAPI.Services.IServices;
using MathZ.Services.ServiceDefaults;
using MathZ.Shared.Data;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddScoped<IJwtGeneratorService, JwtGeneratorService>()
            .AddScoped<IAuthService, AuthService>();

        builder.AddNpgsqlDbContext<UsersDbContext>("usersDb");

        builder.Services.AddIdentity<UserAccount, IdentityRole>()
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddDefaultTokenProviders();

        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "AuthAPI",
                Description = "An ASP.NET Core Web API for managing user accounts authorization and registration",
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
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}