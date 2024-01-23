using Microsoft.EntityFrameworkCore;
using Tailoring.Data;
using Tailoring.Entities;

namespace Tailoring.Repository;

public class EntityFrameworkRepository(TailoringContext dbContext) : IPostsRepository
{
    public async Task CreateAsync(Post game)
    {
        dbContext.Posts.Add(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Posts.Where(game => game.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Post?> GetAsync(int id)
    {
        return await dbContext.Posts.FindAsync(id);

    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await dbContext.Posts.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(Post updatedPost)
    {
        dbContext.Update(updatedPost);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> SearchAsync(string query)
    {
        IQueryable<Post> data = dbContext.Posts;

        return await data.
            Where(data => data.Description.Contains(query) || data.Title.Contains(query) || data.Category.Contains(query) )
            
            .ToListAsync();
    }
    public async Task<IEnumerable<Post>> GetWithCategoryAsync(string category)
    {
        IQueryable<Post> data = dbContext.Posts;

        return await data.Where(data => data.Category.Contains(category)).ToListAsync();
    }
}