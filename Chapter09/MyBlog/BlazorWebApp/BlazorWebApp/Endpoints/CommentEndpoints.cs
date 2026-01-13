using BlazorWebApp.Client.Models;
using BlazorWebApp.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BlazorWebApp.Endpoints;

public static class CommentEndpoints
{
    public static void MapCommentApi(this WebApplication app)
    {
        app.MapGet("/api/Comments/{*blogPostid}",
            async (IBlogRepository repository, string blogPostid) =>
            {
                return Results.Ok(await repository.GetCommentsAsync(blogPostid));
            }).RequireAuthorization();

        app.MapPut("/api/Comments",
            async (IBlogRepository repository, [FromBody] Comment item) =>
            {
                return Results.Ok(await repository.SaveCommentAsync(item));
            }).RequireAuthorization();

        app.MapDelete("/api/Comments/{*id}",
            async (IBlogRepository repository, string id) =>
            {
                await repository.DeleteCommentAsync(id);
                return Results.Ok();
            });
    }
}
