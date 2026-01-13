var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
     .WithLifetime(ContainerLifetime.Persistent)
     .WithDataVolume(isReadOnly: false)
     .WithPgAdmin();

var myBlogDatabase = postgres.AddDatabase("myBlogDb");

builder.AddProject<Projects.BlazorWebApp>("blazorwebapp")
    .WithReference(myBlogDatabase)
    .WaitFor(postgres);

builder.Build().Run();
