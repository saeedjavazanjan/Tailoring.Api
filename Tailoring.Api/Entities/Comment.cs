using System.ComponentModel.DataAnnotations;

namespace Tailoring.Entities;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public  int UserId { get; set; }
    
    [Required]
    public  int PostId { get; set; }
    
    [Required]
    public  long Date { get; set; }
    [Required]
    public required string CommentText { get; set; }
    public required string Avatar { get; set; }
    public required string UserName { get; set; }
}

