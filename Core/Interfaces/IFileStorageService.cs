using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> UploadFile(string containerName, IFormFile file);
        Task<string> EditFile(string containerName, string oldFileRoute, IFormFile newFile);
    }
}