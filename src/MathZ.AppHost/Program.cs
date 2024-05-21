#pragma warning disable SA1124 // Do not use regions

// test github actions

using MathZ.AppHost;
using MathZ.Shared;
using TraceLens.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

#region Trace tracking
builder.AddTraceLens();
#endregion

#region Database passwords
var databasePasswords = builder.Configuration.GetSection("DatabasePasswords");
var identityDatabasePassword = databasePasswords["Identity"];
var emailDatabasePassword = databasePasswords["Email"];
var forumDatabasePassword = databasePasswords["Forum"];
#endregion

#region Message bus
var messageBus = builder
    .AddRabbitMQ(AspireConnections.MessageBuss.RabbitMQ)
    .WithManagementPlugin();
#endregion

#region Services
#region Identity
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
#endregion

#region Email
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
#endregion

#region Forum
var forumDatabase = builder
    .AddPostgres(AspireConnections.Database.ForumDatabaseServer, password: builder.CreateStablePassword(forumDatabasePassword!))
    .WithPgAdmin()
    .WithDataVolume(AspireConnections.Database.ForumDatabase)
    .AddDatabase(AspireConnections.Database.ForumDatabase);

var forumApi = builder
    .AddProject<Projects.MathZ_Services_ForumApi>(AspireConnections.Api.ForumApi)
    .WithJwt("JwtOptions")
    .WithReference(forumDatabase);
#endregion
#endregion

#region Reverse Proxy
builder.AddYarp(AspireConnections.ApiGateway)
    .WithHttpEndpoint(port: 8081)
    .WithReference(identityApi)
    .WithReference(emailApi)
    .WithReference(forumApi)
    .LoadFromConfiguration("ReverseProxy");

await builder.Build().RunAsync();
#endregion
