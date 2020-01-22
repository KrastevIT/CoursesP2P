using CoursesP2P.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.Models.BindingModels
{
    public class AddLecturesBindingModel
    {

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        public IFormFile Video { get; set; }
    }
}
