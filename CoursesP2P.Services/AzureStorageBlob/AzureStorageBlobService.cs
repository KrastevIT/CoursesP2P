using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CoursesP2P.Services.AzureStorageBlob
{
    public class AzureStorageBlobService : IAzureStorageBlobService
    {
        private readonly BlobServiceClient blobServiceClient;

        public AzureStorageBlobService(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadImageAsync(IFormFile img)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("img");
            await containerClient.CreateIfNotExistsAsync();

            var fileName = img.Name + Guid.NewGuid();
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders()
            {
                ContentType = img.ContentType
            };

            await blobClient.UploadAsync(img.OpenReadStream(), blobHttpHeaders);

            var path = blobClient.Uri.AbsoluteUri;

            return path;

        }

        public async Task<string> UploadVideoAsync(IFormFile video)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("video");
            await containerClient.CreateIfNotExistsAsync();

            var fileName = video.Name + Guid.NewGuid();
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders()
            {
                ContentType = video.ContentType
            };


            await blobClient.UploadAsync(video.OpenReadStream(), blobHttpHeaders);

            var path = blobClient.Uri.AbsoluteUri;

            return path;

        }
    }
}
