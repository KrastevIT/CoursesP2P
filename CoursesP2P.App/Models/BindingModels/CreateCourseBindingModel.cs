using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.App.Models.BindingModels
{
    public class CreateCourseBindingModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(22)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 5000.00, ErrorMessage = "Invalid Target Price; Max 5000.00")]
        public decimal Price { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(90)]
        public string Skills { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
