using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.App.Models.BindingModels
{
    public class CreateCourseBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }

    }
}
