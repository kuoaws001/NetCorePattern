namespace NetCorePattern.Service
{
    public interface IFileService
    {
        public Tuple<bool, string> SaveImage(IFormFile imageFile);
    }
}
