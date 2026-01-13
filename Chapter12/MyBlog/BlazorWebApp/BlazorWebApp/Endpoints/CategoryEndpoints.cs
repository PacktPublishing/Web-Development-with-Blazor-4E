namespace BlazorWebApp.Endpoints;

using BlazorWebApp.Client.Interfaces;
using BlazorWebApp.Client.Models;
using Microsoft.AspNetCore.Mvc;

public static class CategoryEndpoints
{
    public static void MapCategoryApi(this WebApplication app)
    {
        app.MapGet("/api/Categories",
        async (IBlogRepository repository) =>
        {
            return Results.Ok(await repository.GetCategoriesAsync());
        });
        app.MapGet("/api/Categories/{*id}",
        async (IBlogRepository repository, string id) =>
        {
            return Results.Ok(await repository.GetCategoryAsync(id));
        });

        app.MapPut("/api/Categories",
        async (IBlogRepository repository, [FromBody] Category item) =>
        {
            return Results.Ok(await repository.SaveCategoryAsync(item));
        }).RequireAuthorization();

        app.MapDelete("/api/Categories/{*id}",
        async (IBlogRepository repository, string id) =>
        {
            await repository.DeleteCategoryAsync(id);
            return Results.Ok();
        }).RequireAuthorization();
    }
}
