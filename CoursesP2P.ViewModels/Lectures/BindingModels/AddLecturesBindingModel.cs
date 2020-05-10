using Courses.P2P.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Lectures.BindingModels
{
    public class AddLecturesBindingModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Video]
        [BytesSizeLimit(1200000000)]
        public IFormFile Video { get; set; }
    }
}
