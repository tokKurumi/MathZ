var builder = DistributedApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection("jwtOptions");

var jwtAudience = jwtOptions["Audience"] ?? "mathz.client";
var jwtIssuer = jwtOptions["Issuer"] ?? "mathz.services.authapi";
var jwtSecret = jwtOptions["Secret"] ?? Guid.NewGuid().ToString();

var usersDb = builder.AddPostgresContainer("mathz.databases.users")
    .WithVolumeMount("mathz.databases.users", "/var/lib/postgresql/data/", VolumeMountType.Named);

var authApi = builder.AddProject<Projects.MathZ_Services_AuthAPI>("mathz.services.authapi")
    .WithReference(usersDb)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var distributionAPI = builder.AddProject<Projects.MathZ_Services_DistributionAPI>("mathz.services.distributionapi")
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var userApi = builder.AddProject<Projects.MathZ_Services_UserAPI>("mathz.services.userapi")
    .WithReference(usersDb)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

var adminApi = builder.AddProject<Projects.MathZ_Services_AdminAPI>("mathz.services.adminapi")
    .WithReference(usersDb)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_SECRET", jwtSecret);

builder.Build().Run();