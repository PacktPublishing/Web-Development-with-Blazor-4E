using BlazorWebApp.Client.Models;
using BlazorWebApp.Client.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints;

public static class BlogPostEndpoints
{
    public static void MapBlogPostApi(this WebApplication app)
    {
        app.MapGet("/api/BlogPosts",
            async (IBlogRepository repository, [FromQuery] int numberofposts, [FromQuery] int startindex) =>
            {
                return Results.Ok(await repository.GetBlogPostsAsync(numberofposts, startindex));
            });
        app.MapGet("/api/BlogPostCount",
            async (IBlogRepository repository) =>
            {
                return Results.Ok(await repository.GetBlogPostCountAsync());
            });
        app.MapGet("/api/BlogPosts/{*id}",
           async (IBlogRepository repository, string id) =>
           {
               return Results.Ok(await repository.GetBlogPostAsync(id));
           });

        app.MapPut("/api/BlogPosts",
            async (IBlogRepository repository, [FromBody] BlogPost item) =>
            {
                return Results.Ok(await repository.SaveBlogPostAsync(item));
            }).RequireAuthorization();

        app.MapDelete("/api/BlogPosts/{*id}",
            async (IBlogRepository repository, string id) =>
            {
                await repository.DeleteBlogPostAsync(id);
                return Results.Ok();
            }).RequireAuthorization();
    }

}
