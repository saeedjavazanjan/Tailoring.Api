using System.ComponentModel.DataAnnotations;

namespace Tailoring.Entities;

public class User
{
    
    public int UserId { get; set; }

    [Required][StringLength(50)]
    public required string UserName { get; set; }
    
    [Required][StringLength(4)]
    public required string PssWord { get; set; }
  
    [Required][StringLength(12)]
    public required string PhoneNumber { get; set; }
    public required string Avatar { get; set; }
    
    [StringLength(500)]
    public required string Bio { get; set; }
    public int  Followers { get; set; }
    public int Followings { get; set; }
    public required List<int> Likes { get; set; }
    public required List<int> Bookmarks { get; set; }
    
    
}


