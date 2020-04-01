using CoursesP2P.ViewModels.ReCaptcha;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoursesP2P.Services.ReCaptcha
{
    public class ReCAPTCHAService : IReCAPTCHAService
    {
        private readonly ReCAPTCHASettings settings;

        public ReCAPTCHAService(IOptions<ReCAPTCHASettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task<ReCAPTCHAResponse> Verify(string token)
        {
            var myData = new ReCAPTCHAData
            {
                Response = token,
                Secret = this.settings.ReCAPTCHA_Secret_Key
            };

            var client = new HttpClient();

            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={myData.Secret}&response={myData.Response}");

            var capResp = JsonConvert.DeserializeObject<ReCAPTCHAResponse>(response);

            return capResp;
        }
    }
}
