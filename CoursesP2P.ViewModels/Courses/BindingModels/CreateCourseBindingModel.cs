using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Courses.BindingModels
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
        [MaxLength(1000)]
        public string Skills { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public string InstructorFullName { get; set; }

        public string InstructorId { get; set; }
    }
}
