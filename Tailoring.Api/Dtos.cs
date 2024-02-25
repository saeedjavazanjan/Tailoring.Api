using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;
using Tailoring.Entities;

namespace Tailoring;

public record PostDto(
    int Id,
    string Title,
    string Category,
    string PostType,
    string Author,
    int AuthorId,
    string AuthorAvatar,
   List< string> FeaturedImages,
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
    [Required]long Date,
    [Required] string CommentText
);

public record UserDto(
 int UserId,
 [Required][StringLength(50)] string UserName,
 [Required][StringLength(10)] string Password,
 [Required][StringLength(12)] string PhoneNumber,
 string Avatar,
 string Bio,
 int Followers,
 int Followings,
 List<int> Likes,
 List<int> Bookmarks
     );

        

public record UserUpdateDto(
    [Required][StringLength(50)] string UserName,
    IFormFile? AvatarFile,
    string Bio
);


public record RegisterUserDto(
    [Required][StringLength(50)] string UserName,
    [Required][StringLength(12)] string PhoneNumber
);



public record AddUserDto(
     [StringLength(50)] string UserName,
    [Required] [StringLength(4)] string Password,
    [Required] [StringLength(12)] string PhoneNumber
);


public record CreatePostDto(
    [Required] [StringLength(100)] string Title,
    [Required] string Category,
   [Required] string PostType,
    IFormFileCollection?  FeaturedImages,
     IFormFile? Video,
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