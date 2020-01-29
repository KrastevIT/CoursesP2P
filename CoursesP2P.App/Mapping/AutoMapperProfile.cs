using AutoMapper;
using CoursesP2P.App.Models.BindingModels;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Models;
using System.Linq;

namespace CoursesP2P.App.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseEnrolledViewModel>();

            CreateMap<Lecture, CourseLecturesViewModel>();

            CreateMap<AddLecturesBindingModel, Lecture>();
        }
    }
}
