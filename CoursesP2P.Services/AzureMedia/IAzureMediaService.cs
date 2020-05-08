using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Management.Media.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.AzureMedia
{
    public interface IAzureMediaService
    {
        Task<Asset> CreateInputAssetAsync(IFormFile video);

        Task<Asset> CreateOutputAssetAsync();

        Task<Transform> GetOrCreateTransformAsync();

        Task<Job> SubmitJobAsync(string inputAssetName, string outputAssetName, string transformName);

        Task<Job> WaitForJobToFinishAsync(string transformName, string jobName);

        Task<StreamingLocator> CreateStreamingLocatorAsync(string assetName);

        Task<IList<string>> GetStreamingUrlsAsync(string locatorName);

        Task CleanUpAsync(string transformName, string assetName);
    }
}
