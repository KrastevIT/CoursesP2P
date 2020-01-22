using System.Collections.Generic;

namespace CoursesP2P.App.Models.ViewModels
{
    public class VideoViewModel
    {
        public string Name { get; set; }

        public string VideoPath { get; set; }

        public ICollection<VideoLectureViewModel> Lectures { get; set; } = new HashSet<VideoLectureViewModel>();
    }
}
