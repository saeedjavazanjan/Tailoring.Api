namespace Tailoring.Repository;

public interface IFileService
{
    public Tuple<int, string> SaveAvatar(IFormFile avatarFile);
    public bool DeleteAvatar(string imageFileName);
    
}