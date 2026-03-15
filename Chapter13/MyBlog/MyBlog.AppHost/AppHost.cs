var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgres("postgres")
     .WithLifetime(ContainerLifetime.Persistent)
     .WithDataVolume(isReadOnly: false)
     .WithPgAdmin();

var myBlogDatabase = postgres.AddDatabase("myBlogDb");

builder.AddProject<Projects.BlazorWebApp>("blazorwebapp")
    .WithEndpoint("https", endpoint =>
    {
        endpoint.Port = 7119;
        endpoint.IsProxied = false;
    })
    .WithEndpoint("http", endpoint =>
    {
        endpoint.Port = 5025;
        endpoint.IsProxied = false;
    })
    .WithReference(myBlogDatabase)
    .WaitFor(postgres);

builder.AddProject<Projects.RootLevelCascadingValueDemo>("rootlevelcascadingvaluedemo");

builder.Build().Run();
