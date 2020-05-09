using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Reviews
{
    public class ReviewBindingModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public IFormFile Video { get; set; }
    }
}
