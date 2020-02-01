using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels
{
    public class VideoViewModel
    {
        public string Name { get; set; }

        public string Video { get; set; }

        public IEnumerable<VideoLectureViewModel> Lectures { get; set; } = new HashSet<VideoLectureViewModel>();
    }
}
