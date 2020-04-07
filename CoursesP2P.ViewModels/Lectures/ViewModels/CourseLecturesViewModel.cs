using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;

namespace CoursesP2P.ViewModels.Lectures.ViewModels
{
    public class CourseLecturesViewModel : IMapFrom<Lecture>
    {
        public string Name { get; set; }
    }
}
