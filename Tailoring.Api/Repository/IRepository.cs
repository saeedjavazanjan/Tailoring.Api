using System.Runtime.InteropServices.JavaScript;
using Tailoring.Entities;

namespace Tailoring.Repository;

public interface IRepository
{
    //posts
    Task CreateAsync(Post post);
    Task DeleteAsync(int id);

    Task<Post?> GetAsync(int id);

    Task<IEnumerable<Post>> GetAllAsync();

    Task UpdateAsync(Post updatedPost);

    Task<IEnumerable<Post>> SearchAsync(string query);

    Task<IEnumerable<Post>> GetWithCategoryAsync(string category);
    
    //comments
    Task AddCommentAsync(Comment comment);
    Task DeleteComments(int id);
    Task<IEnumerable<Comment>> GetPostCommentsAsync(int postId);

    Task UpdateCommentAsync(Comment comment);

    Task<Comment?> GetCommentAsync(int id);

    
    //users
    Task AddUser (User user);
    Task<User?> GetRegesteredPhoneNumberAsync(string phoneNumber);
    Task<User?> GetUserAsync(int id);
    Task DeleteUser(int id);
    Task UpdateUserAsync(User user);


}