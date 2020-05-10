using Courses.P2P.Common.Attributes;
using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Courses.BindingModels
{
    public class CreateCourseBindingModel
    {
        [Required(ErrorMessage = "Моля, въведете име")]
        [MinLength(1)]
        [MaxLength(200)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Моля, въведете описание")]
        [MinLength(22, ErrorMessage = "Трябва да е минимум 22 символа")]
        [MaxLength(500)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, въведете цена")]
        [Range(0, 5000.00, ErrorMessage = "Максимална цена 5000.00")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Моля, въведете умения")]
        [MinLength(3, ErrorMessage = "Трябва да е минимум 3 символа")]
        [MaxLength(1000)]
        [Display(Name = "Умения")]
        public string Skills { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Required]
        [Image(ErrorMessage = "Невалиден формат")]
        [BytesSizeLimit(22000000, ErrorMessage = "Изображението е твърде голямо")]
        public IFormFile Image { get; set; }

        public DateTime CreatedOn { get; set; }

        public string InstructorFullName { get; set; }

        public string InstructorId { get; set; }
    }
}
