using CloudinaryDotNet.Actions;
using CoursesP2P.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CoursesP2PDbContext db;
        private readonly CloudinaryDotNet.Cloudinary cloudinary;

        public CloudinaryService(CoursesP2PDbContext db, CloudinaryDotNet.Cloudinary cloudinary)
        {
            this.db = db;
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
    }
}
