using MassTransit;
using MathZ.ServiceDefaults;
using MathZ.Services.IdentityApi.Data;
using MathZ.Services.IdentityApi.MappingProfiles;
using MathZ.Services.IdentityApi.Services;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared;
using MathZ.Shared.Models;
using MathZ.Shared.ServicesHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, config) =>
    {
        var connectionString = builder.Configuration.GetConnectionString(AspireConnections.MessageBuss.RabbitMQ);

        config.Host(connectionString);
    });
});

builder.AddNpgsqlDbContext<UserIdentityDbContext>(AspireConnections.Database.IdentityDatabase);
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHostedService<DatabaseInitializerService>();

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

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<IdentityProfile>();
});
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();

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
