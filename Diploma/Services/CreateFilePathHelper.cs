using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class CreateFilePathHelper : IService, IGetStraightPath, ICombinePath
    {
        private IWebHostEnvironment _webHostEnvironment;

        public CreateFilePathHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetStraightPath(params string[] directories)
        {
            var directoriesPath = GetCombinePath(directories);
            var straightPath = GetCombinePath(_webHostEnvironment.WebRootPath, directoriesPath);
            return straightPath;
        }

        public string GetCombinePath(params string[] directories)
        {
            return Path.Combine(directories);
        }
    }
}
