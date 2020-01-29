using AutoMapper;
using CoursesP2P.App.Models.ViewModels;
using CoursesP2P.Models;

namespace CoursesP2P.App.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseEnrolledViewModel>();
        }
    }
}
