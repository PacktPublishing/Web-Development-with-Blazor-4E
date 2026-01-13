using BlazorWebApp.Client.Models;
using BlazorWebApp.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BlazorWebApp.Endpoints;

public static class TagEndpoints
{
    public static void MapTagApi(this WebApplication app)
    {
        app.MapGet("/api/Tags",
        async (IBlogRepository repository) =>
        {
            return Results.Ok(await repository.GetTagsAsync());
        });
        app.MapGet("/api/Tags/{*id}",
        async (IBlogRepository repository, string id) =>
        {
            return Results.Ok(await repository.GetTagAsync(id));
        });
        app.MapPut("/api/Tags",
        async (IBlogRepository repository, [FromBody] Tag item) =>
        {
            return Results.Ok(await repository.SaveTagAsync(item));
        }).RequireAuthorization(); app.MapDelete("/api/Tags/{*id}",
        async (IBlogRepository repository, string id) =>
        {
            await repository.DeleteTagAsync(id);
            return Results.Ok();
        }).RequireAuthorization();

    }
}
