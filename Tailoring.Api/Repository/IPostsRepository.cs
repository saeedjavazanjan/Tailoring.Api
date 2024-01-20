﻿using Tailoring.Entities;

namespace Tailoring.Repository;

public interface IPostsRepository
{
    Task CreateAsync(Post post);
    Task DeleteAsync(int id);

    Task<Post?> GetAsync(int id);

    Task<IEnumerable<Post>> GetAllAsync();

    Task UpdateAsync(Post updatedPost);

    Task<IEnumerable<Post>> SearchAsync(string query,PostParams postParams);

    Task<IEnumerable<Post>> GetWithCategoryAsync(string category);
}