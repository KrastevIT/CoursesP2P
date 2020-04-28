using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Lectures.ViewModels
{
    public class VideoViewModel : IMapFrom<Lecture>
    {
        public string Name { get; set; }

        public string Video { get; set; }

        public IEnumerable<VideoLectureViewModel> Lectures { get; set; } = new HashSet<VideoLectureViewModel>();
    }
}
