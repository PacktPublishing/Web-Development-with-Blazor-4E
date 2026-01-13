using BlazorWebApp.Client.Models;
using BlazorWebApp.Client.Interfaces;
namespace BlazorServer.Services;

public class BlazorServerBlogNotificationService : IBlogNotificationService
{
    public event Action<BlogPost>? BlogPostChanged;
    public Task SendNotification(BlogPost post)
    {
        BlogPostChanged?.Invoke(post);
        return Task.CompletedTask;
    }
}
