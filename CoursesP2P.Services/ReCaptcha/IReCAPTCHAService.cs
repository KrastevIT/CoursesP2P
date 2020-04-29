using CoursesP2P.ViewModels.ReCaptcha;
using System.Threading.Tasks;

namespace CoursesP2P.Services.ReCaptcha
{
    public interface IReCAPTCHAService
    {
        public Task<ReCAPTCHAResponse> VerifyAsync(string token);
    }
}
