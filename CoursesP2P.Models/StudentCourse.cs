namespace CoursesP2P.Models
{
    public class StudentCourse
    {
        public string StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
