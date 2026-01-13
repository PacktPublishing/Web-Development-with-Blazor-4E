var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
var app = builder.Build();

app.MapDefaultEndpoints();
app.UseCors();
app.MapGet("/", () => "Hello World!");
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });
app.UseBlazorFrameworkFiles();
app.Run();
