using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryDotNet.Cloudinary cloudinary;

        public CloudinaryService(CloudinaryDotNet.Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            byte[] imageByte;
            string path = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageByte = memoryStream.ToArray();
            }

            using (var destinationSteam = new MemoryStream(imageByte))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.Name, destinationSteam)
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                path = uploadResult.Uri.AbsoluteUri;
            }

            return path;
        }

        public string UploadVideo(IFormFile video)
        {
            string path = string.Empty;
            using (var stream = video.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(video.Name, stream),
                    EagerAsync = true
                };

                Task<VideoUploadResult> task = Task<VideoUploadResult>.Run(() =>
                {
                    var uploadResult = cloudinary.UploadAsync(uploadParams);
                    return uploadResult;
                });
                var result = task.Result;
                path = result.Uri.AbsoluteUri;
            }

            return path;
        }
    }
}
