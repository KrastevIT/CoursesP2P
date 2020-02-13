using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Lectures.BindingModels
{
    public class AddLecturesBindingModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public IFormFile Video { get; set; }
    }
}
