namespace Tailoring.Repository;

public interface IFileService
{
    public Tuple<int, string> SaveAvatar(IFormFile avatarFile,String userId);
    public Tuple<int, string> SavePostVideo(IFormFile postVideo,String postId);
    public Tuple<int, List<string>> SavePostImages(IFormFileCollection postImages,String postId);
    public Tuple<int, List<string>> SaveProductImages(IFormFileCollection postImages,String postId);
    public bool DeleteAvatar(string userId);
    public bool DeletePostImage(string postId);
    public bool DeleteProductImage(string productId);
    public bool DeletePostVideo(string postId);
    
    
    
}