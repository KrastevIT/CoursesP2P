using CoursesP2P.Models.Enum;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseEnrolledViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        //Id = course.Id,
        //            Name = course.Name,
        //            Category = course.Category,
        //            Image = course.Image,
        //            InstructorFullName = course.InstructorFullName,
        //            Lectures = lectures
    }
}
