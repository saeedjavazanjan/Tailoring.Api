using System.ComponentModel.DataAnnotations;

namespace Tailoring;

public record PostDto(
    int Id,
    string Title,
    string Category,
    string PostType,
    string Author,
    int AuthorId,
    string AuthorAvatar,
    List<string> FeaturedImages,
    int  Like,
    string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    int  HaveProduct
);

public record CommentDto(
    int Id,
    int UserId,
    int PostId,
    long Date,
    string CommentText,
    string Avatar,
    string UserName
);


public record AddCommentDto(
    [Required] int UserId,
    [Required] int PostId,
    [Required]long Date,
    [Required] string CommentText
    
);

public record UpdateCommentDto(
    [Required] int UserId,
    [Required] int PostId,
    [Required]long Date,
    [Required] string CommentText
);

public record CreatePostDto(
    [Required] [StringLength(100)] string Title,
    [Required] string Category,
   [Required] string PostType,
    [Required] string Author,
    [Required] int AuthorId,
    string AuthorAvatar,
    List<string> FeaturedImages,
    [Required] int Like,
     string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    [Required] int HaveProduct
);

public record UpdatePostDto(
    [Required] [StringLength(100)] string Title,
    [Required] string Category,
    [Required] string PostType,
    [Required] string Author,
    [Required] int AuthorId,
    string AuthorAvatar,
    List<string> FeaturedImages,
    [Required] int Like,
    string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    [Required] int HaveProduct
);