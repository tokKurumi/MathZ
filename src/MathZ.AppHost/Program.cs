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

// Message bus
var messageBus = builder.AddKafka(AspireConnections.MessageBuss.Kafka);

// Identity
var identityDatabase = builder.AddPostgres(AspireConnections.Database.IdentityDatabaseServer, password: identityDatabasePassword)
    .WithPgAdmin()
    .WithVolumeMount(AspireConnections.Database.IdentityDatabase, "/var/lib/postgresql/data")
    .AddDatabase(AspireConnections.Database.IdentityDatabase);

var identityApi = builder.AddProject<Projects.MathZ_Services_IdentityApi>(AspireConnections.Api.IdentityApi)
    .WithJwt(jwtSecret, jwtAudience, jwtIssuer)
    .WithReference(identityDatabase)
    .WithReference(messageBus);

// Email
var emailDatabase = builder.AddPostgres(AspireConnections.Database.EmailDatabaseServer, password: emailDatabasePassword)
    .WithPgAdmin()
    .WithVolumeMount(AspireConnections.Database.EmailDatabase, "/var/lib/postgresql/data")
    .AddDatabase(AspireConnections.Database.EmailDatabase);

var emailEpi = builder.AddProject<Projects.MathZ_Services_EmailApi>(AspireConnections.Api.EmailApi)
    .WithJwt(jwtSecret, jwtAudience, jwtIssuer)
    .WithSmtp(smtpFrom, smtpHost, smtpPort, smtpUserName, smtpPassword)
    .WithReference(emailDatabase)
    .WithReference(messageBus);

await builder.Build().RunAsync();
