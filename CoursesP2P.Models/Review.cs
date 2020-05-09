namespace CoursesP2P.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string VideoUrl { get; set; }

        public string Asset { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
