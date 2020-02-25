using CoursesP2P.ViewModels.Home;

namespace CoursesP2P.Services.Home
{
    public interface IHomeService
    {
        HomeInfoAndCoursesViewModel GetAllInfoWithCourses();
    }
}
