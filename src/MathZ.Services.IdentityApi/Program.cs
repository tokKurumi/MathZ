using MathZ.ServiceDefaults;
using MathZ.Services.IdentityApi.Data;
using MathZ.Services.IdentityApi.MappingProfiles;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Services;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<UserIdentityDbContext>(AspireConnections.Database.IdentityDatabase);
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHostedService<DatabaseInitializerService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<IdentityProfile>();
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AuthAPI",
        Description = "An ASP.NET Core Web API for managing user identities",
        Contact = new OpenApiContact
        {
            Name = "Yudashkin Oleg",
            Url = new Uri(@"https://github.com/tokKurumi"),
            Email = @"tokkurumi901@gmail.com",
        },
    });

    config.UseDtoConversion();
    config.UseOAuth2();
    config.UseDocumentation();
});
builder.Services.AddCustomApiVersioning();
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.EnablePersistAuthorization();
        config.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
