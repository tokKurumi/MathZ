var builder = DistributedApplication.CreateBuilder(args);

var usersDb = builder.AddPostgresConnection("usersDb", "Host=192.168.50.143; Database=MathZ.Identity; Username=kurumi; Password=T0qatlc1UDxRDiJfGrYJ8ChRPe2uI5zz");

builder.AddProject<Projects.MathZ_Services_AuthAPI>("authapi")
    .WithReference(usersDb);

builder.Build().Run();