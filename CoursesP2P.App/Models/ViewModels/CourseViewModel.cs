using CoursesP2P.Models.Enum;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public string InstructorFullName { get; set; }

        public int Orders { get; set; }

    }
}
