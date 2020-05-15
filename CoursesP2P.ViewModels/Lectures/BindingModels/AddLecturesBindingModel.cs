using Courses.P2P.Common;
using Courses.P2P.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Lectures.BindingModels
{
    public class AddLecturesBindingModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredName)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredVideo)]
        [Video]
        [BytesSizeLimit(12000000000)]
        public IFormFile Video { get; set; }
    }
}
