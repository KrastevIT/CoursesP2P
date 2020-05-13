using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;

namespace CoursesP2P.ViewModels.Lectures.ViewModels
{
    public class CourseLecturesViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Video { get; set; }
    }
}
