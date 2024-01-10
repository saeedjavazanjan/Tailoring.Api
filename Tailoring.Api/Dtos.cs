using System.ComponentModel.DataAnnotations;

namespace Tailoring;

public record GameDto(
    int Id,
    string Title,
    string Category,
    string PostType,
    string Author,
    int AuthorId,
    List<string> FeaturedImages,
    int  Like,
    string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    int  HaveProduct
);

public record CreatePostDto(
    [Required] [StringLength(20)] string Title,
    [Required] string Category,
   [Required] string PostType,
    [Required] string Author,
    [Required] int AuthorId,
    List<string> FeaturedImages,
    [Required] int Like,
     string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    [Required] int HaveProduct
);

public record UpdatePostDto(
    [Required] [StringLength(20)] string Title,
    [Required] string Category,
    [Required] string PostType,
    [Required] string Author,
    [Required] int AuthorId,
    List<string> FeaturedImages,
    [Required] int Like,
    string Video,
    string Description,
    DateTime DataAdded,
    long LongDataAdded,
    [Required] int HaveProduct
);