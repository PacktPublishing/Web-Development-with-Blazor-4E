using BlazorWebApp.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApp.Database;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the many-to-many relationship between BlogPost and Tag
        modelBuilder.Entity<BlogPost>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.BlogPosts)
            .UsingEntity(j => j.ToTable("BlogPostTags")); // Join table for many-to-many

        // Other model configurations can be placed here
    }
}
