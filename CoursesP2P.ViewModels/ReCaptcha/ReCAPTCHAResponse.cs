using System;

namespace CoursesP2P.ViewModels.ReCaptcha
{
    public class ReCAPTCHAResponse
    {
        public bool Success { get; set; }

        public double Score { get; set; }

        public string Action { get; set; }

        public DateTime Challenge_Ts { get; set; }

        public string HostName { get; set; }
    }
}
