using Tailoring.Entities;

namespace Tailoring.Response;

public class UserResponse
{
    public int Status { get; set; }

    public required string Message { get; set; }
    
    public User? User  { get; set; }
}