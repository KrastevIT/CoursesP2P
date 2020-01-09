using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoursesP2P.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public ICollection<StudentCourse> EnrolledCourses { get; set; } = new HashSet<StudentCourse>();

        public ICollection<Course> CreatedCourses { get; set; } = new HashSet<Course>();
    }
}
