using System.Collections.Generic;

namespace CoursesP2P.Models
{
    public class Instructor : User
    {
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

    }
}
