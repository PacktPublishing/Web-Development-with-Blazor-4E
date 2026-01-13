using BlazorWebApp.Client;
using BlazorWebApp.Client.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using BlazorWebApp.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddTransient<IBlogRepository, BlogApiWebClient>();
builder.Services.AddHttpClient("Api", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<BlogBrowserStorage>();
builder.Services.AddSingleton<IBlogNotificationService, BlazorWebAssemblyBlogNotificationService>();
await builder.Build().RunAsync();
