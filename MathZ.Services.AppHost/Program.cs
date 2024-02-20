var builder = DistributedApplication.CreateBuilder(args);

// JWT
var jwtOptions = builder.Configuration.GetSection("JwtOptions");
var jwtAudience = jwtOptions["Audience"] ?? "mathz.client";
var jwtIssuer = jwtOptions["Issuer"] ?? "mathz.services.authapi";
var jwtSecret = jwtOptions["Secret"] ?? Guid.NewGuid().ToString();

// SMTP
var smtpConfiguration = builder.Configuration.GetSection("SMTP");
var smtpFrom = smtpConfiguration["From"] ?? "dontreply@mathz.com";
var smtpHost = smtpConfiguration["Host"] ?? "smtp.gmail.com";
var smtpPort = smtpConfiguration["Port"] ?? "465";
var smtpUserName = smtpConfiguration["UserName"] ?? "mathz.contact@gmail.com";
var smtpPassword = smtpConfiguration["Password"] ?? "icekxmpldxvbqqqm";

// Databases
var usersDb = builder.AddPostgresContainer("mathz.databases.users")
    .WithVolumeMount("mathz.databases.users", "/var/lib/postgresql/data/", VolumeMountType.Named);

// Micro-services
var emailApi = builder.AddProject<Projects.MathZ_Services_EmailAPI>("mathz.services.emailapi")
    .WithEnvironment("SMTP_FROM", smtpFrom)
    .WithEnvironment("SMTP_HOST", smtpHost)
    .WithEnvironment("FROM_PORT", smtpPort)
    .WithEnvironment("FROM_USERNAME", smtpUserName)
    .WithEnvironment("FROM_PASSWORD", smtpPassword)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var userApi = builder.AddProject<Projects.MathZ_Services_UserAPI>("mathz.services.userapi")
    .WithReference(usersDb)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var authApi = builder.AddProject<Projects.MathZ_Services_AuthAPI>("mathz.services.authapi")
    .WithReference(usersDb)
    .WithReference(emailApi)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var adminApi = builder.AddProject<Projects.MathZ_Services_AdminAPI>("mathz.services.adminapi")
    .WithReference(usersDb)
    .WithReference(authApi)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var distributionAPI = builder.AddProject<Projects.MathZ_Services_DistributionAPI>("mathz.services.distributionapi")
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var statisticsAPI = builder.AddProject<Projects.MathZ_Services_StatisticsAPI>("mathz.services.statisticsapi")
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

builder.Build().Run();