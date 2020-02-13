using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Lectures
{
    public interface ILectureService
    {
        Task<IEnumerable<LectureViewModel>> GetLecturesByCourseId(int id, ClaimsPrincipal instructor);

        void Add(AddLecturesBindingModel model);

        VideoViewModel GetVideoByLectureId(int id);
    }
}
