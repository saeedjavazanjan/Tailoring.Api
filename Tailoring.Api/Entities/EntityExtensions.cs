namespace Tailoring.Entities;

public static class EntityExtensions
{
    public static PostDto AsDto(this Post post){
        return new PostDto(
            post.Id,
            post.Title,
            post.Category,
            post.PostType,
            post.Author,
            post.AuthorId,
            post.FeaturedImages,
            post.Like,
            post.Video,
            post.Description,
            post.DataAdded,
            post.LongDataAdded,
            post.HaveProduct
            
        );


    }  
}