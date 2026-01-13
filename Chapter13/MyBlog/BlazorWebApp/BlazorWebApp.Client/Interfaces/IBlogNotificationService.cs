using BlazorWebApp.Client.Models;
namespace BlazorWebApp.Client.Interfaces;

public interface IBlogNotificationService
{
    event Action<BlogPost>? BlogPostChanged;
    Task SendNotification(BlogPost post);
}
