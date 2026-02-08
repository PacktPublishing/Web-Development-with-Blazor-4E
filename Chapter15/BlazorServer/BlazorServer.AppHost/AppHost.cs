var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BlazorServer>("blazorserver");

builder.Build().Run();
