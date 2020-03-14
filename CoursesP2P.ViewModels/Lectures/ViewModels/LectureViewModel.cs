namespace CoursesP2P.ViewModels.Lectures.ViewModels
{
    public class LectureViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName =>
            this.Name?.Length > 20
            ? this.Name.Substring(0, 20) + "..."
            : this.Name;

        public string Presentation { get; set; }

        public string Video { get; set; }
    }
}
