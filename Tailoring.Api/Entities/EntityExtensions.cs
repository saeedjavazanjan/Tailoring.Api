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
            post.AuthorAvatar,
            post.FeaturedImages,
            post.Like,
            post.Video,
            post.Description,
            post.DataAdded,
            post.LongDataAdded,
            post.HaveProduct
            
        );


    }

    public static CommentDto AsDto(this Comment comment)
    {
        return new CommentDto(
            comment.Id,
            comment.UserId,
            comment.PostId,
            comment.Date,
            comment.CommentText,
            comment.Avatar,
            comment.UserName
        );
    }
}