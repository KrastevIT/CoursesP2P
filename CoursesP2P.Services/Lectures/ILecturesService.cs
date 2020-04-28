using CoursesP2P.Models;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.Services.Lectures
{
    public interface ILecturesService
    {
        IEnumerable<LectureViewModel> GetLecturesByCourseIdAsync(int courseId, string userId, bool isAdmin);

        void Add(AddLecturesBindingModel model);

        VideoViewModel GetVideoByLectureId(int lectureId, string userId);

        AddLecturesBindingModel GetLectureBindingModelWithCourseId(int courseId, User user);
    }
}
