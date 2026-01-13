using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebAppTest;
using WebAppTest.Client.Pages;
using WebAppTest.Client.RenderModes;
using WebAppTest.Client.Services;
using WebAppTest.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<RenderModeSelector>();
builder.Services.AddScoped<WeatherService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/set-render-mode/{mode}", (string mode, RenderModeSelector renderModeSelector) =>
{
    if (Enum.TryParse<ComponentRenderMode>(mode, true, out var parsedMode))
    {
        renderModeSelector.SelectedRenderMode = parsedMode switch
        {
            ComponentRenderMode.ServerPrerendered => RenderMode.InteractiveServer,
            ComponentRenderMode.WasmPrerendered => RenderMode.InteractiveWebAssembly,
            ComponentRenderMode.AutoPrerendered => RenderMode.InteractiveAuto,
            ComponentRenderMode.Server => new InteractiveServerRenderMode(false),
            ComponentRenderMode.Wasm => new InteractiveWebAssemblyRenderMode(false),
            ComponentRenderMode.Auto => new InteractiveAutoRenderMode(false),
            ComponentRenderMode.SSR => null,
            _ => RenderMode.InteractiveAuto
        };
        return Results.Redirect("/Counter");
    }
    throw new Exception("Render mode not found");
});
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebAppTest.Client._Imports).Assembly);

app.Run();
