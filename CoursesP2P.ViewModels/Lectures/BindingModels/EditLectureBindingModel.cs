using Courses.P2P.Common;
using Courses.P2P.Common.Attributes;
using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.ViewModels.Lectures.BindingModels
{
    public class EditLectureBindingModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredName)]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Asset { get; set; }

        public string Video { get; set; }

        [Video]
        [BytesSizeLimit(1200000000)]
        public IFormFile VideoUpload { get; set; }
    }
}
