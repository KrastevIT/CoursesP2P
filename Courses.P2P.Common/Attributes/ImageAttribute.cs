﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Courses.P2P.Common.Attributes
{
    public class ImageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var image = (IFormFile)value;

            if (!image.ContentType.Contains("image"))
            {
                return false;
            }

            return true;
        }
    }
}
