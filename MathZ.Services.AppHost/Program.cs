var builder = DistributedApplication.CreateBuilder(args);

var usersDb = builder.AddPostgresContainer("users")
    .WithVolumeMount("mathz.databases.users", "/var/lib/postgresql/data/", VolumeMountType.Named)
    .AddDatabase("usersDb");

var authApi = builder.AddProject<Projects.MathZ_Services_AuthAPI>("mathz.services.authapi")
    .WithReference(usersDb);

var adminApi = builder.AddProject<Projects.MathZ_Services_AdminAPI>("mathz.services.adminapi")
    .WithReference(authApi);

//var gatewayApi = builder.AddProject<Projects.MathZ_GatewayAPI>("mathz.gatewayapi")
//    .WithReference(authApi)
//    .WithReference(usersDb);

builder.Build().Run();