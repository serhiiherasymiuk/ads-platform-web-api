using Azure.Storage.Blobs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> EditFile(string containerName, string oldFileName, IFormFile newFile)
        {
            await DeleteFile(containerName, oldFileName);
            return await UploadFile(containerName, newFile);
        }

        public async Task<string> UploadFile(string containerName, IFormFile file)
        {
            var client = new BlobContainerClient(connectionString, containerName);

            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            BlobClient blob = client.GetBlobClient(file.GetHashCode() + "_" + file.FileName);
            await blob.UploadAsync(file.OpenReadStream());
            return blob.Uri.ToString();
        }
    }
}