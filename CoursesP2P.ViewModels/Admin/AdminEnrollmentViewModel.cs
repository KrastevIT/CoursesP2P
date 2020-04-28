using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;

namespace CoursesP2P.ViewModels.Admin
{
    public class AdminEnrollmentViewModel : IMapFrom<StudentCourse>
    {
        public string StudentId { get; set; }

        public int CourseId { get; set; }

    }
}
