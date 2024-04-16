using MailKit.Net.Smtp;
using MassTransit;
using MathZ.ServiceDefaults;
using MathZ.Services.EmailApi.Data;
using MathZ.Services.EmailApi.MappingProfiles;
using MathZ.Services.EmailApi.Services;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared;
using MathZ.Shared.ServicesHelpers;
using MathZ.Shared.Smtp;
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

builder.AddNpgsqlDbContext<MailingDbContext>(AspireConnections.Database.EmailDatabase);
builder.Services.AddHostedService<DatabaseInitializerService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EmailProfile>();
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EmailAPI",
        Description = "An ASP.NET Core Web API for managing email",
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

builder.Services.AddSingleton(serviceProvider =>
{
    var smtpHost = builder.Configuration.GetValue<string>(SmtpEnvConstants.Host) ?? string.Empty;
    var smtpPort = builder.Configuration.GetValue<int>(SmtpEnvConstants.Port);
    var smtpUserName = builder.Configuration.GetValue<string>(SmtpEnvConstants.UserName) ?? string.Empty;
    var smtpPassword = builder.Configuration.GetValue<string>(SmtpEnvConstants.Password) ?? string.Empty;

    var smtp = new SmtpClient();
    smtp.Connect(smtpHost, smtpPort, true);
    smtp.Authenticate(smtpUserName, smtpPassword);

    return smtp;
});
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IMailingService, MailingService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

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
