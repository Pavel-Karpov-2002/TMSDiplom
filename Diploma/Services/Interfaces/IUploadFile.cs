namespace Diploma.Services.Interfaces
{
    public interface IUploadFile
    {
        public Task<bool> UploadFile(string filePath, IFormFile file);
    }
}
