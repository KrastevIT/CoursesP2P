using Courses.P2P.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Reviews
{
    public class ReviewBindingModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [Video(ErrorMessage = "Невалиден формат")]
        [BytesSizeLimit(12000000000)]
        public IFormFile Video { get; set; }
    }
}
