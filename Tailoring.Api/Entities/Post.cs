using System.ComponentModel.DataAnnotations;

namespace Tailoring.Entities;

public class Post
{
    public int Id { get; set; }

    [Required][StringLength(100)]
    public required string Title { get; set; }
    
    [Required]
    public required string Category { get; set; }
   
    [Required]
    public required string PostType { get; set; }
  
    [Required]
    public required string Author { get; set; }
  
    [Required]
    public int  AuthorId { get; set; }
    
    public required string AuthorAvatar { get; set; }

   
    public required List<string> FeaturedImages { get; set; }
  
    [Required]
    public int  Like { get; set; }
    
    public required string Video { get; set; }
    
    public required string Description { get; set; }
    
    
    public DateTime DataAdded { get; set; }
    
    public long LongDataAdded { get; set; }
    
    [Required]
    public int  HaveProduct { get; set; }
    
    
}
