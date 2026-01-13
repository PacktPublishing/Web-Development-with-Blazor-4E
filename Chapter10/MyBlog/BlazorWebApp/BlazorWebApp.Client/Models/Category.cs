using System.ComponentModel.DataAnnotations;

namespace BlazorWebApp.Client.Models;

public class Category
{
    public string? Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}
