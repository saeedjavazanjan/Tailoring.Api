using System.ComponentModel.DataAnnotations;

namespace Tailoring.Entities;

public class Product
{
    
     public int Id { get; set; }
     [Required]
     public required string Name { get; set; }
     [Required]
     public required string Description { get; set; }
     [Required]
     public required string TypeOfProduct { get; set; }
     [Required]
     public required string Mas { get; set; }
     [Required]
     public required string Supply { get; set; }
     [Required]
     public required string Unit { get; set; }
     [Required]
     public required string Price { get; set; }
     [Required]
     public required int PostId { get; set; }
     public List<String> Images { get; set; }
     public string AttachedFile { get; set; }
    
    
}