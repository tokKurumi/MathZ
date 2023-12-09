var builder = DistributedApplication.CreateBuilder(args);

string jwtAudience = @"mathz-client";
string jwtIssuer = "mathz-auth-api";
string jwtSecret = @"642477c9-7840-4563-b0a2-02c677eb2db0";

var usersDb = builder.AddPostgresContainer("users")
    .WithVolumeMount("mathz.databases.users", "/var/lib/postgresql/data/", VolumeMountType.Named)
    .AddDatabase("usersDb");

var authApi = builder.AddProject<Projects.MathZ_Services_AuthAPI>("mathz.services.authapi")
    .WithReference(usersDb)
    .WithEnvironment("JwtOptions:Audience", jwtAudience)
    .WithEnvironment("JwtOptions:Issuer", jwtIssuer)
    .WithEnvironment("JwtOptions:Secret", jwtSecret);

var adminApi = builder.AddProject<Projects.MathZ_Services_AdminAPI>("mathz.services.adminapi")
    .WithReference(usersDb)
    .WithEnvironment("JwtOptions:Audience", jwtAudience)
    .WithEnvironment("JwtOptions:Issuer", jwtIssuer)
    .WithEnvironment("JwtOptions:Secret", jwtSecret);

builder.Build().Run();