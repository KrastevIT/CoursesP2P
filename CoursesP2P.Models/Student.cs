using System.Collections.Generic;

namespace CoursesP2P.Models
{
    public class Student : User
    {
        public ICollection<StudentCourse> EnrolledCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
