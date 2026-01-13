var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BlazorWebApp>("blazorwebapp");

builder.Build().Run();
