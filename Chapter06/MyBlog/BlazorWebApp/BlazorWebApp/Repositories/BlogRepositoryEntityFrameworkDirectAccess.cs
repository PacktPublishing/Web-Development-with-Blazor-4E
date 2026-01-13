using BlazorWebApp.Client.Interfaces;
using BlazorWebApp.Client.Models;
using BlazorWebApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace BlazorWebApp.Repositories;

public class BlogRepositoryEntityFrameworkDirectAccess(IDbContextFactory<BlogDbContext> factory) : IBlogRepository
{
    public async Task<BlogPost?> GetBlogPostAsync(string id)
    {
        using var context = factory.CreateDbContext();
        //Convert id to an int
        if (int.TryParse(id, out int intid))
        {
            var item = await context.BlogPosts.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == intid);
            if (item != null)
            {
                return ConvertBlogPostToDto(item);
            }
        }
        return null;

    }

    public async Task<List<BlogPost>> GetBlogPostsAsync(int numberofposts, int startindex)
    {
        using var context = factory.CreateDbContext();
        return await context.BlogPosts.OrderByDescending(p => p.PublishDate).Skip(startindex).Take(numberofposts).Select(p => ConvertBlogPostToDto(p)).ToListAsync();
    }

    public async Task<int> GetBlogPostCountAsync()
    {
        using var context = factory.CreateDbContext();
        return await context.BlogPosts.CountAsync();
    }


    public async Task<List<Category>> GetCategoriesAsync()
    {
        using var context = factory.CreateDbContext();
        return await context.Categories.Select(c => ConvertCategoryToDto(c)!).ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(string id)
    {
        using var context = factory.CreateDbContext();
        return ConvertCategoryToDto(await context.Categories.FirstOrDefaultAsync(c => c.Id == Convert.ToInt32(id)));

    }

    public async Task<Tag?> GetTagAsync(string id)
    {
        using var context = factory.CreateDbContext();
        var item = await context.Tags.FirstOrDefaultAsync(t => t.Id == Convert.ToInt32(id));
        if (item != null)
        {
            return ConvertTagToDto(item);
        }
        return null;
    }

    public async Task<List<Tag>> GetTagsAsync()
    {
        using var context = factory.CreateDbContext();
        return await context.Tags.Select(t => ConvertTagToDto(t)).ToListAsync();
    }


    public async Task<Comment?> GetCommentAsync(string id)
    {
        using var context = factory.CreateDbContext();
        var item = await context.Comments.FirstOrDefaultAsync(t => t.Id == Convert.ToInt32(id));
        if (item != null)
        {
            return ConvertCommentToDto(item);
        }
        return null;
    }


    public async Task DeleteBlogPostAsync(string id)
    {
        await DeleteItemAsync<Database.Entities.BlogPost>(id);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await DeleteItemAsync<Database.Entities.Category>(id);
    }

    public async Task DeleteTagAsync(string id)
    {
        await DeleteItemAsync<Database.Entities.Tag>(id);
    }

    public async Task DeleteCommentAsync(string id)
    {
        await DeleteItemAsync<Database.Entities.Comment>(id);
    }


    private async Task DeleteItemAsync<T>(string id) where T:class
    {
        if (int.TryParse(id, out int intid))
        {
            using var context = factory.CreateDbContext();
            var item = await context.Set<T>().FindAsync(intid);
            if (item != null)
            {
                context.Set<T>().Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }

    private static Tag ConvertTagToDto(BlazorWebApp.Database.Entities.Tag item)
    {
        return new Tag() { Id = item.Id.ToString(), Name = item.Name };
    }

    private static Comment ConvertCommentToDto(BlazorWebApp.Database.Entities.Comment item)
    {
        return new Comment() { Id = item.Id.ToString(), Text = item.Text, Date = item.Date, BlogPostId = item.BlogPostId.ToString(), Name = item.Name };
    }

    private static Category? ConvertCategoryToDto(BlazorWebApp.Database.Entities.Category? item)
    {
        if (item == null)
        {
            return null;
        }
        return new Category() { Id = item.Id.ToString(), Name = item.Name };
    }

    private static BlogPost ConvertBlogPostToDto(BlazorWebApp.Database.Entities.BlogPost item)
    {
        Category? category = null;
        if (item.Category != null)
        {
            category = new Category() { Id = item.Category.Id.ToString(), Name = item.Category.Name };
        }
        return new BlogPost()
        {
            Id = item.Id.ToString(),
            Title = item.Title,
            Text = item.Text,
            PublishDate = item.PublishDate,
            Category = category,
            Tags = item.Tags.Select(t => new Tag() { Id = t.Id.ToString(), Name = t.Name }).ToList()
        };
    }


    private async Task<BlogPost> SaveItem(BlogPost item)
    {
        using var context = factory.CreateDbContext();
        if (item.Id == null)
        {
            var newitem = new BlazorWebApp.Database.Entities.BlogPost()
            {
                Title = item.Title,
                Text = item.Text,
                PublishDate = item.PublishDate,
                CategoryId = item.Category == null ? null : int.Parse(item.Category.Id!),
            };

            foreach (var tag in item.Tags)
            {
                var t = await context.Tags.FirstOrDefaultAsync(t => t.Id == Convert.ToInt32(tag.Id));
                if (t != null)
                {
                    newitem.Tags.Add(t);
                }
            }

            context.Add(newitem);
            await context.SaveChangesAsync();
            item.Id = newitem.Id.ToString();
            return item;
        }
        else
        {
            var existingitem = await context.BlogPosts.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(item.Id));
            if (existingitem != null)
            {
                existingitem.Title = item.Title;
                existingitem.Text = item.Text;
                existingitem.PublishDate = item.PublishDate;
                existingitem.CategoryId = item.Category == null ? null : int.Parse(item.Category.Id!);

                existingitem.Tags.Clear();
                foreach (var tag in item.Tags)
                {
                    var t = await context.Tags.FirstOrDefaultAsync(t => t.Id == Convert.ToInt32(tag.Id));
                    if (t != null)
                    {
                        existingitem.Tags.Add(t);
                    }
                }
                await context.SaveChangesAsync();
                return item;
            }
        }
        return item;
    }


    private async Task<Tag> SaveItem(Tag item)
    {
        using var context = factory.CreateDbContext();
        if (item.Id == null)
        {
            var newitem = new BlazorWebApp.Database.Entities.Tag()
            {
                Name = item.Name,
            };
            context.Add(newitem);
            await context.SaveChangesAsync();
            item.Id = newitem.Id.ToString();
            return item;
        }
        else
        {
            var existingitem = await context.Tags.FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(item.Id));
            if (existingitem != null)
            {
                existingitem.Name = item.Name;
                await context.SaveChangesAsync();
                return item;
            }
        }
        return item;
    }
    private async Task<Category> SaveItem(Category item)
    {
        using var context = factory.CreateDbContext();
        if (item.Id == null)
        {
            var newitem = new BlazorWebApp.Database.Entities.Category()
            {
                Name = item.Name,
            };
            context.Add(newitem);
            await context.SaveChangesAsync();
            item.Id = newitem.Id.ToString();
            return item;
        }
        else
        {
            var existingitem = await context.Categories.FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(item.Id));
            if (existingitem != null)
            {
                existingitem.Name = item.Name;
                await context.SaveChangesAsync();
                return item;
            }
        }
        return item;
    }

    private async Task<Comment> SaveItem(Comment item)
    {
        using var context = factory.CreateDbContext();
        if (item.Id == null)
        {
            var newitem = new BlazorWebApp.Database.Entities.Comment()
            {
                BlogPostId = int.Parse(item.BlogPostId),
                Text = item.Text,
                Date = item.Date,
                Name = item.Name
            };
            context.Add(newitem);
            await context.SaveChangesAsync();
            item.Id = newitem.Id.ToString();
            return item;
        }
        else
        {
            var existingitem = await context.Comments.FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(item.Id));
            if (existingitem != null)
            {
                existingitem.Text = item.Text;
                await context.SaveChangesAsync();
                return item;
            }
        }
        return item;
    }


    public async Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
    {
        return await SaveItem(item);
    }

    public async Task<Category?> SaveCategoryAsync(Category item)
    {
        return await SaveItem(item);
    }

    public async Task<Tag?> SaveTagAsync(Tag item)
    {
        return await SaveItem(item);
    }

    public async Task<Comment?> SaveCommentAsync(Comment item)
    {
        return await SaveItem(item);
    }

    public async Task<List<Comment>> GetCommentsAsync(string blogPostId)
    {
        using var context = factory.CreateDbContext();
        int id = Convert.ToInt32(blogPostId);
        return await context.Comments.Where(c => c.BlogPostId == id).Select(t => ConvertCommentToDto(t)).ToListAsync();
    }
}