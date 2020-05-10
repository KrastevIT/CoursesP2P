using Courses.P2P.Common;
using Courses.P2P.Common.Attributes;
using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Courses.BindingModels
{
    public class CreateCourseBindingModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredName)]
        [MinLength(1)]
        [MaxLength(200)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredDescription)]
        [MinLength(22, ErrorMessage = ErrorMessages.DescriptionLength)]
        [MaxLength(500)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredPrice)]
        [Range(0, 5000.00)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredSkills)]
        [MinLength(3, ErrorMessage = ErrorMessages.SkillLength)]
        [MaxLength(1000)]
        [Display(Name = "Умения")]
        public string Skills { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredImage)]
        [Image(ErrorMessage = ErrorMessages.InvalidImageFormat)]
        [BytesSizeLimit(22000000, ErrorMessage = ErrorMessages.ImageLength)]
        public IFormFile Image { get; set; }

        public DateTime CreatedOn { get; set; }

        public string InstructorFullName { get; set; }

        public string InstructorId { get; set; }
    }
}
