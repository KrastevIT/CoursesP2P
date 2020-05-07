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

        public int CourseId { get; set; }

        public IFormFile Video { get; set; }
    }
}
