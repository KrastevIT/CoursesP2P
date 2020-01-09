namespace CoursesP2P.Models
{
    public class Lecture
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Presentation { get; set; }

        public string Video { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
