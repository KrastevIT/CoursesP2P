using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Courses.P2P.Common.Attributes
{
    public class BytesSizeLimitAttribute : ValidationAttribute
    {
        public BytesSizeLimitAttribute(long bytes)
        {
            this.Bytes = bytes;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var formFile = (IFormFile)value;
            if (formFile == null || formFile.Length > this.Bytes)
            {
                return false;
            }

            return true;
        }

        public long Bytes { get; set; }
    }
}
