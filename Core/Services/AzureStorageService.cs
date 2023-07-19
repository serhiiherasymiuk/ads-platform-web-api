using Azure.Storage.Blobs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;
        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnectionString");
        }

        public async Task DeleteFile(string containerName, string fileName)
        {
            var client = new BlobContainerClient(connectionString, containerName);

            if (!await client.ExistsAsync()) return;

            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(string containerName, string oldFileName, string fileName, IFormFile newFile)
        {
            await DeleteFile(containerName, oldFileName);
            return await UploadFile(containerName, newFile, fileName);
        }

        public async Task<string> UploadFile(string containerName, IFormFile file, string fileName)
        {
            var client = new BlobContainerClient(connectionString, containerName);

            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            BlobClient blob = client.GetBlobClient(fileName);
            await blob.UploadAsync(file.OpenReadStream());
            return blob.Uri.ToString();
        }
    }
}