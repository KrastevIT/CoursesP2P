using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Http;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }
    }
}
