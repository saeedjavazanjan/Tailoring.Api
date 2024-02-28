using Microsoft.EntityFrameworkCore;
using Tailoring.Authentication;
using Tailoring.Data;
using Tailoring.Entities;

namespace Tailoring.Repository;

public class EntityFrameworkRepository(TailoringContext dbContext) : IRepository
{
    

    
    public async Task CreateAsync(Post post)
    {
        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Posts.Where(post => post.Id == id)
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

        return await data.Where(data =>
                data.Description.Contains(query) || data.Title.Contains(query) || data.Category.Contains(query))

            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetWithCategoryAsync(string category)
    {
        IQueryable<Post> data = dbContext.Posts;

        return await data.Where(data => data.Category.Contains(category)).ToListAsync();
    }

    //comments
    public async Task AddCommentAsync(Comment comment)
    {
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteComments(int id)
    {
        await dbContext.Comments.Where(comment => comment.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Comment>> GetPostCommentsAsync(int postId)
    {
        return await dbContext.Comments.Where(comment => comment.PostId == postId)
            .ToListAsync();
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        dbContext.Update(comment);
        await dbContext.SaveChangesAsync();
    }
    public async Task<Comment?> GetCommentAsync(int id)
    {
        return await dbContext.Comments.FindAsync(id);

    }
    
    //users

    public async Task AddUser(User user)
    {
         dbContext.Users.Add(user);
         await dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetRegesteredPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<User?> GetUserAsync(int id)
    {
        return await dbContext.Users.FindAsync(id);

    }
    
    public async Task DeleteUser(int id)
    {
        await dbContext.Users.Where(user => user.UserId == id)
            .ExecuteDeleteAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        dbContext.Update(user);
        await dbContext.SaveChangesAsync();
        
    }
    
    //products
    public async Task CreateProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<Product?> GetProductAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);

    }
    public async Task UpdateProductAsync(Product updatedProduct)
    {
        dbContext.Update(updatedProduct);
        await dbContext.SaveChangesAsync();
    }
}

/*public async Task getUserAvatar(int UserId)
{
    return await dbContext.
}
}*/