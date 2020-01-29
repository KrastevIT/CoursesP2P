using AutoMapper;
using CoursesP2P.App.Models.BindingModels.Lecture;
using CoursesP2P.App.Models.ViewModels.Course;
using CoursesP2P.Models;

namespace CoursesP2P.App.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseViewModel>();

            CreateMap<Course, CourseEnrolledViewModel>();


            CreateMap<Lecture, CourseLecturesViewModel>();

            CreateMap<AddLecturesBindingModel, Lecture>();
        }
    }
}
