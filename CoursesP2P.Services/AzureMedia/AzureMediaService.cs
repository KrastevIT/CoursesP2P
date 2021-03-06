﻿using CoursesP2P.ViewModels.AzureMedia;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.AzureMedia
{
    public class AzureMediaService : IAzureStorageBlob
    {
        private AzureMediaSettings options;

        public AzureMediaService(IOptions<AzureMediaSettings> options)
        {
            this.options = options.Value;
        }

        //Качва файла в контейнера в хранилището, използвайки URL адреса на SAS.
        public async Task<Asset> CreateInputAssetAsync(IFormFile video)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";
            string assetName = Guid.NewGuid().ToString();

            Asset asset = await client.Assets.CreateOrUpdateAsync(resourceGroupName, accountName, assetName, new Asset());

            var response = await client.Assets.ListContainerSasAsync(
                resourceGroupName,
                accountName,
                assetName,
                permissions: AssetContainerPermission.ReadWrite,
                expiryTime: DateTime.UtcNow.AddHours(4).ToUniversalTime());

            var sasUri = new Uri(response.AssetContainerSasUrls.First());

            var videoName = Guid.NewGuid().ToString();

            CloudBlobContainer container = new CloudBlobContainer(sasUri);
            var blob = container.GetBlockBlobReference(videoName);
            blob.Properties.ContentType = video.ContentType;

            await blob.UploadFromStreamAsync(video.OpenReadStream());

            return asset;
        }


        //Създайте изходен актив, който да съхранява резултата от задание
        public async Task<Asset> CreateOutputAssetAsync()
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";
            string assetName = Guid.NewGuid().ToString();

            Asset outputAsset = await client.Assets.GetAsync(resourceGroupName, accountName, assetName);
            Asset asset = new Asset();
            string outputAssetName = assetName;

            if (outputAsset != null)
            {
                string uniqueness = $"-{Guid.NewGuid().ToString("N")}";
                outputAssetName += uniqueness;

                Console.WriteLine("Warning – found an existing Asset with name = " + assetName);
                Console.WriteLine("Creating an Asset with this name instead: " + outputAssetName);
            }

            return await client.Assets.CreateOrUpdateAsync(resourceGroupName, accountName, outputAssetName, asset);
        }

        //Създайте Трансформация и работа, която кодира качения файл
        public async Task<Transform> GetOrCreateTransformAsync()
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";
            string transformName = Guid.NewGuid().ToString();

            Transform transform = await client.Transforms.GetAsync(resourceGroupName, accountName, transformName);

            if (transform == null)
            {
                TransformOutput[] output = new TransformOutput[]
                {
            new TransformOutput
            {
                Preset = new BuiltInStandardEncoderPreset()
                {
                    PresetName = EncoderNamedPreset.AdaptiveStreaming
                }
            }
                };

                transform = await client.Transforms.CreateOrUpdateAsync(resourceGroupName, accountName, transformName, output);
            }

            return transform;
        }

        //Работа
        public async Task<Job> SubmitJobAsync(string inputAssetName, string outputAssetName, string transformName)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";
            string jobName = Guid.NewGuid().ToString();

            JobInput jobInput = new JobInputAsset(assetName: inputAssetName);

            JobOutput[] jobOutputs =
            {
                new JobOutputAsset(outputAssetName),
            };
            Job job = await client.Jobs.CreateAsync(
                resourceGroupName,
                accountName,
                transformName,
                jobName,
                new Job
                {
                    Input = jobInput,
                    Outputs = jobOutputs,
                });

            return job;
        }

        //Изчакайте работата да приключи
        public async Task<Job> WaitForJobToFinishAsync(string transformName, string jobName)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";

            const int SleepIntervalMs = 60 * 1000;

            Job job = null;

            do
            {
                job = await client.Jobs.GetAsync(resourceGroupName, accountName, transformName, jobName);

                Console.WriteLine($"Job is '{job.State}'.");
                for (int i = 0; i < job.Outputs.Count; i++)
                {
                    JobOutput output = job.Outputs[i];
                    Console.Write($"\tJobOutput[{i}] is '{output.State}'.");
                    if (output.State == JobState.Processing)
                    {
                        Console.Write($"  Progress: '{output.Progress}'.");
                    }

                    Console.WriteLine();
                }

                if (job.State != JobState.Finished && job.State != JobState.Error && job.State != JobState.Canceled)
                {
                    await Task.Delay(SleepIntervalMs);
                }
            }
            while (job.State != JobState.Finished && job.State != JobState.Error && job.State != JobState.Canceled);

            return job;
        }

        //Вземете локален канал за потоци
        public async Task<StreamingLocator> CreateStreamingLocatorAsync(string assetName)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";
            string locatorName = Guid.NewGuid().ToString();

            StreamingLocator locator = await client.StreamingLocators.CreateAsync(
                resourceGroupName,
                accountName,
                locatorName,
        new StreamingLocator
        {
            AssetName = assetName,
            StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly
        });

            return locator;
        }

        //Вземете поточни URL адреси
        public async Task<IList<string>> GetStreamingUrlsAsync(string locatorName)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";

            const string DefaultStreamingEndpointName = "default";

            IList<string> streamingUrls = new List<string>();

            StreamingEndpoint streamingEndpoint = await client.StreamingEndpoints.GetAsync(resourceGroupName, accountName, DefaultStreamingEndpointName);

            if (streamingEndpoint != null)
            {
                if (streamingEndpoint.ResourceState != StreamingEndpointResourceState.Running)
                {
                    await client.StreamingEndpoints.StartAsync(resourceGroupName, accountName, DefaultStreamingEndpointName);
                }
            }

            ListPathsResponse paths = await client.StreamingLocators.ListPathsAsync(resourceGroupName, accountName, locatorName);

            foreach (StreamingPath path in paths.StreamingPaths)
            {
                UriBuilder uriBuilder = new UriBuilder();
                uriBuilder.Scheme = "https";
                uriBuilder.Host = streamingEndpoint.HostName;

                uriBuilder.Path = path.Paths[0];
                streamingUrls.Add(uriBuilder.ToString());
            }

            return streamingUrls;
        }

        //Почистете ресурсите във вашия акаунт за Media Services
        public async Task CleanUpAsync(string transformName, string assetName)
        {
            IAzureMediaServicesClient client = await CreateMediaServicesClientAsync();

            string resourceGroupName = "coursesP2P";
            string accountName = "coursesp2p";

            await client.Assets.DeleteAsync(resourceGroupName, accountName, assetName);
        }

        private async Task<ServiceClientCredentials> GetCredentialsAsync()
        {
            ClientCredential clientCredential = new ClientCredential(this.options.ClientId, this.options.ClientSecret);
            return await ApplicationTokenProvider.LoginSilentAsync("coursesp2pgmail.onmicrosoft.com", clientCredential, ActiveDirectoryServiceSettings.Azure);
        }

        private async Task<IAzureMediaServicesClient> CreateMediaServicesClientAsync()
        {
            var credentials = await GetCredentialsAsync();

            return new AzureMediaServicesClient(new Uri("https://management.azure.com"), credentials)
            {
                SubscriptionId = this.options.SubscriptionId
            };
        }
    }
}
