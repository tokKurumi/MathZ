using MathZ.ServiceDefaults;
using MathZ.Services.ForumApi.Data;
using MathZ.Services.ForumApi.Services;
using MathZ.Shared;
using MathZ.Shared.ServicesHelpers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<ForumDbContext>(AspireConnections.Database.ForumDatabase);
builder.Services.AddHostedService<DatabaseInitializerService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ForumAPI",
        Description = "An ASP.NET Core Web API for managing forum",
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

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

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
