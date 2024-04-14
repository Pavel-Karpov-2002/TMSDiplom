using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class UploadFileHelper : IService, IUploadFile
    {
        public async Task<bool> UploadFile(string filePath, IFormFile file)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return true;
        }
    }
}
