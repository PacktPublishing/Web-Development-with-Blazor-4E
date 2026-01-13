using YamlDotNet.Core.Tokens;

var builder = DistributedApplication.CreateBuilder(args);

var blazorBackend = builder.AddProject<Projects.BlazorWebApplication>("blazorwebapplication");

builder.AddJavaScriptApp("AngularFrontend", "../AngularFrontend","start")    
    .WithHttpEndpoint(env: "PORT")
    .WithEnvironment("BLAZOR_BASE_URL", blazorBackend.GetEndpoint("https"))
    .WithExternalHttpEndpoints();


builder.AddJavaScriptApp("ReactFrontend", "../ReactFrontend", "dev")
    .WithHttpEndpoint(env: "PORT")
    .WithEnvironment("BLAZOR_BASE_URL", blazorBackend.GetEndpoint("https"))
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.RazorPagesProject>("RazorPagesProject");

builder.AddProject<Projects.BlazorProject>("blazorproject");

builder.Build().Run();
