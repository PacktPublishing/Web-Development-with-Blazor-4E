using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebAppTest.Client.RenderModes;
using WebAppTest.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<WeatherService>();
await builder.Build().RunAsync();
