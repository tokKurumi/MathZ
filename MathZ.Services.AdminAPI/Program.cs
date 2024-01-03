namespace MathZ.Services.AdminAPI;

using System.ComponentModel;
using System.Reflection;
using AutoMapper;
using MathZ.Services.AdminAPI.Services;
using MathZ.Services.AdminAPI.Services.IServices;
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

        builder.Services.AddScoped<IAdminService, AdminService>();

        builder.Services.AddHttpClient<IAdminService, AdminService>(client =>
        {
            client.BaseAddress = new Uri(@"http://mathz.services.authapi");
        });

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
            options.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName ?? x.Name);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
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