namespace Tailoring.Repository;

public class FileService:IFileService
{
    private IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }


    public Tuple<int, string> SaveAvatar(IFormFile avatarFile)
    {
        try
        {
            var contentPath = this._environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Avatars"); 
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(avatarFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                return new Tuple<int, string>(0, msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            // we are trying to create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            avatarFile.CopyTo(stream);
            stream.Close();
            return new Tuple<int, string>(1, newFileName);
        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }    }

    public bool DeleteAvatar(string imageFileName)
    {
        try
        {
            var wwwPath = this._environment.WebRootPath;
            var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }    }
}