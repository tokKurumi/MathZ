using MathZ.AppHost;
using MathZ.Shared;

var builder = DistributedApplication.CreateBuilder(args);

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
    .WithJwt("JwtOptions")
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
    .WithJwt("JwtOptions")
    .WithSmtp("SMTP")
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
    .WithJwt("JwtOptions")
    .WithReference(forumDatabase);

builder.AddYarp(AspireConnections.ApiGateway)
    .WithHttpEndpoint(port: 8080)
    .WithReference(identityApi)
    .WithReference(emailApi)
    .WithReference(forumApi)
    .LoadFromConfiguration("ReverseProxy");

await builder.Build().RunAsync();
