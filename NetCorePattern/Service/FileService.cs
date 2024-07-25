
namespace NetCorePattern.Service
{
    public class FileService : IFileService
    {
        public Tuple<bool, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var path = @"D:\\images";
                path = Path.Combine(path, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<bool, string>(false, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<bool, string>(true, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, "Error has occured");
            }
        }
    }
}
