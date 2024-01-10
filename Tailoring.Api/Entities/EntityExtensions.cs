namespace Tailoring.Entities;

public static class EntityExtensions
{
    public static GameDto AsDto(this Post post){
        return new GameDto(
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