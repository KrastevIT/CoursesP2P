using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Courses.P2P.Common.Attributes
{
    public class VideoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var video = (IFormFile)value;
            if (video == null || !video.ContentType.Contains("video"))
            {
                return false;
            }

            return true;
        }
    }
}
