using BlazorWebApp.Client.Models;
using BlazorWebApp.Client.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorWebApp.Client;

public class BlogApiWebClient(IHttpClientFactory factory) : IBlogRepository
{
    public async Task<BlogPost?> GetBlogPostAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<BlogPost>($"api/BlogPosts/{id}");
    }
    public async Task<int> GetBlogPostCountAsync()
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<int>("/api/BlogPostCount");
    }
    public async Task<List<BlogPost>> GetBlogPostsAsync(int numberofposts, int startindex)
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<List<BlogPost>>($"/api/BlogPosts?numberofposts={numberofposts}&startindex={startindex}") ?? [];
    }

    public async Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
    {
        var httpclient = factory.CreateClient("Api");
        var response = await httpclient.PutAsJsonAsync<BlogPost>("api/BlogPosts", item);
        return await response.Content.ReadFromJsonAsync<BlogPost>();
    }
    public async Task DeleteBlogPostAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        await httpclient.DeleteAsync($"api/BlogPosts/{id}");
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<List<Category>>($"api/Categories") ?? [];
    }
    public async Task<Category?> GetCategoryAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<Category>($"api/Categories/{id}");
    }
    public async Task DeleteCategoryAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        await httpclient.DeleteAsync($"api/Categories/{id}");
    }
    public async Task<Category?> SaveCategoryAsync(Category item)
    {
        var httpclient = factory.CreateClient("Api");
        var response = await httpclient.PutAsJsonAsync<Category>("api/Categories", item);
        return await response.Content.ReadFromJsonAsync<Category>();
    }
    public async Task<Tag?> GetTagAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<Tag>($"api/Tags/{id}");
    }
    public async Task<List<Tag>> GetTagsAsync()
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<List<Tag>>($"api/Tags") ?? [];
    }
    public async Task DeleteTagAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        await httpclient.DeleteAsync($"api/Tags/{id}");
    }
    public async Task<Tag?> SaveTagAsync(Tag item)
    {
        var httpclient = factory.CreateClient("Api");
        var response = await httpclient.PutAsJsonAsync<Tag>("api/Tags", item);
        return await response.Content.ReadFromJsonAsync<Tag>();
    }
    public async Task<List<Comment>> GetCommentsAsync(string blogpostid)
    {
        var httpclient = factory.CreateClient("Api");
        return await httpclient.GetFromJsonAsync<List<Comment>>($"api/Comments/{blogpostid}") ?? [];
    }

    public async Task DeleteCommentAsync(string id)
    {
        var httpclient = factory.CreateClient("Api");
        await httpclient.DeleteAsync($"api/Comments/{id}");
    }
    public async Task<Comment?> SaveCommentAsync(Comment item)
    {
        var httpclient = factory.CreateClient("Api");
        var response = await httpclient.PutAsJsonAsync<Comment>("api/Comments", item);
        return await response.Content.ReadFromJsonAsync<Comment>();
    }

}

