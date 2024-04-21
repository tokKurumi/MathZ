using MathZ.AppHost;
using MathZ.Shared;

var builder = DistributedApplication.CreateBuilder(args);

// JWT
var jwtOptions = builder.Configuration.GetSection("JwtOptions");
var jwtSecret = jwtOptions["Secret"];
var jwtAudience = jwtOptions["Audience"];
var jwtIssuer = jwtOptions["Issuer"];

// SMTP
var smtpOptions = builder.Configuration.GetSection("SMTP");
var smtpFrom = smtpOptions["From"];
var smtpHost = smtpOptions["Host"];
var smtpPort = int.Parse(smtpOptions["Port"] ?? string.Empty);
var smtpUserName = smtpOptions["UserName"];
var smtpPassword = builder.Configuration["SMTP:Password"];

// Database passwords
var databasePasswords = builder.Configuration.GetSection("DatabasePasswords");
var identityDatabasePassword = databasePasswords["Identity"];
var emailDatabasePassword = databasePasswords["Email"];
var forumDatabasePassword = databasePasswords["Forum"];

// Message bus
var messageBus = builder
    .AddRabbitMQ(AspireConnections.MessageBuss.RabbitMQ)
    .WithManagementPlugin();

// Identity
var identityDatabase = builder
    .AddPostgres(AspireConnections.Database.IdentityDatabaseServer, password: builder.CreateStablePassword(identityDatabasePassword!))
    .WithPgAdmin()
    .WithDataVolume(AspireConnections.Database.IdentityDatabase)
    .AddDatabase(AspireConnections.Database.IdentityDatabase);

var identityApi = builder
    .AddProject<Projects.MathZ_Services_IdentityApi>(AspireConnections.Api.IdentityApi)
    .WithJwt(jwtSecret, jwtAudience, jwtIssuer)
    .WithReference(identityDatabase)
    .WithReference(messageBus);

// Email
var emailDatabase = builder
    .AddPostgres(AspireConnections.Database.EmailDatabaseServer, password: builder.CreateStablePassword(emailDatabasePassword!))
    .WithPgAdmin()
    .WithDataVolume(AspireConnections.Database.EmailDatabase)
    .AddDatabase(AspireConnections.Database.EmailDatabase);

var emailApi = builder
    .AddProject<Projects.MathZ_Services_EmailApi>(AspireConnections.Api.EmailApi)
    .WithJwt(jwtSecret, jwtAudience, jwtIssuer)
    .WithSmtp(smtpFrom, smtpHost, smtpPort, smtpUserName, smtpPassword)
    .WithReference(emailDatabase)
    .WithReference(messageBus);

// Forum
var forumDatabase = builder
    .AddPostgres(AspireConnections.Database.ForumDatabaseServer, password: builder.CreateStablePassword(forumDatabasePassword!))
    .WithPgAdmin()
    .WithDataVolume(AspireConnections.Database.ForumDatabase)
    .AddDatabase(AspireConnections.Database.ForumDatabase);

var forumApi = builder
    .AddProject<Projects.MathZ_Services_ForumApi>(AspireConnections.Api.ForumApi)
    .WithJwt(jwtSecret, jwtAudience, jwtIssuer)
    .WithReference(forumDatabase);

builder.AddYarp(AspireConnections.ApiGateway)
    .WithHttpEndpoint(port: 8080)
    .WithReference(identityApi)
    .WithReference(emailApi)
    .WithReference(forumApi)
    .LoadFromConfiguration("ReverseProxy");

await builder.Build().RunAsync();
