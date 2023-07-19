using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> UploadFile(string containerName, IFormFile file, string fileName);
        Task<string> EditFile(string containerName, string oldFileRoute, string fileName, IFormFile newFile);
    }
}