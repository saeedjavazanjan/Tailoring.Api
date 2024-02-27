namespace Tailoring.Repository;

public class FileService:IFileService
{
    private IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    private const string BaseUrl = "http://10.0.2.2:5198/";


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
            return new Tuple<int, string>(1,BaseUrl+ "Avatars/"+newFileName);
        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }
        
    }

    public Tuple<int, string> SavePostVideo(IFormFile postVideo,String postId)
    {
        try
        {
            if (postVideo.Length < 5242880)
            {
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "postsVideos", postId);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(postVideo.FileName);
                var allowedExtensions = new string[] { ".mp3", ".mp4", ".wma" };
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
                postVideo.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, BaseUrl + "postsVideos/" + postId + newFileName);
            }
            else
            {
                return new Tuple<int, string>(0, "فایل ارسالی نباید بیشتر از 500 مگ باشد");

            }

        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }

    }

    public Tuple<int, List<string>> SavePostImages(IFormFileCollection postImages,String postId)
    {
        var contentPath = this._environment.ContentRootPath;
        var path = Path.Combine(contentPath, "postsImages",postId); 
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        List<string> listOfImages = new List<string>();
        foreach (var postImage in postImages)
        {
            try
            {
                var ext = Path.GetExtension(postImage.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                var notAllowedExtensions = new string[] { ".mp3", ".mp4", ".wma" };
                if (!notAllowedExtensions.Contains(ext))
                {


                    if (!allowedExtensions.Contains(ext))
                    {
                        string msg = string.Format("Only {0} extensions are allowed",
                            string.Join(",", allowedExtensions));
                        return new Tuple<int, List<string>>(0, [msg]);
                    }

                    string uniqueString = Guid.NewGuid().ToString();
                    // we are trying to create a unique filename here
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(path, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    postImage.CopyTo(stream);
                    stream.Close();
                    listOfImages.Add(BaseUrl + "postsImages/" + postId + newFileName);
                    // return new Tuple<int, string>(1, newFileName);
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, List<string>>(0, ["Error has occured"]);
            }
        }

        return new Tuple<int, List<string>>(1, listOfImages);

    }

    public bool DeleteAvatar(string imageFileName)
    {
        try
        {
            var wwwPath = this._environment.WebRootPath;
            var path = Path.Combine( "Avatars\\", imageFileName);
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

    public bool DeletePostImage(string imageFileName)
    {
        throw new NotImplementedException();
    }

    public bool DeletePostVideo(string videoFileName)
    {
        throw new NotImplementedException();
    }
}