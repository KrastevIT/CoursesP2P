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
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 5000.00, ErrorMessage = "Invalid Target Price; Max 5000.00")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }

    }
}
