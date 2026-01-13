using System.ComponentModel.DataAnnotations;

namespace BlazorWebApp.Client.Models;

public class Comment
{
    public string? Id { get; set; }
    public required string BlogPostId { get; set; }
    public DateTime Date { get; set; }
    [Required]
    public string Text { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
}
