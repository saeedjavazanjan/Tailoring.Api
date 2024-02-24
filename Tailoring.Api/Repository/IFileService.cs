namespace Tailoring.Repository;

public interface IFileService
{
    public Tuple<int, string> SaveAvatar(IFormFile avatarFile);
    public Tuple<int, string> SavePostVideo(IFormFile postVideo);
    public Tuple<int, string> SavePostImages(List<IFormFile> postImages);
    public bool DeleteAvatar(string imageFileName);
    public bool DeletePostImage(string imageFileName);
    public bool DeletePostVideo(string videoFileName);
    
    
    
}