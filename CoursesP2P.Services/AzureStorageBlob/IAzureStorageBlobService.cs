using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoursesP2P.Services.AzureStorageBlob
{
    public interface IAzureStorageBlobService
    {
        Task<string> UploadImageAsync(IFormFile img);

        Task<string> UploadVideoAsync(IFormFile video);
    }
}
