using CoursesP2P.Models;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Lectures
{
    public interface ILecturesService
    {
        IEnumerable<LectureViewModel> GetLecturesByCourseIdAsync(int id, User user);

        void Add(AddLecturesBindingModel model);

        VideoViewModel GetVideoByLectureId(int id);

        AddLecturesBindingModel GetLectureBindingModelWithCourseId(int courseId, User user);
    }
}
