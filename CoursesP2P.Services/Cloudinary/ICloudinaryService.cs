using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile image);

        string UploadVideo(IFormFile video);
    }
}
