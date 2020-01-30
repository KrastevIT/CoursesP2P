using AutoMapper;
using CoursesP2P.App.Models.BindingModels.Course;
using CoursesP2P.App.Models.BindingModels.Lecture;
using CoursesP2P.App.Models.ViewModels.Course;
using CoursesP2P.App.Models.ViewModels.Lecture;
using CoursesP2P.Models;

namespace CoursesP2P.App.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Course Map
            CreateMap<Course, CourseViewModel>();
            CreateMap<Course, CourseEnrolledViewModel>();
            CreateMap<Course, CourseDetailsViewModel>();
            CreateMap<Course, CourseInstructorViewModel>();
            CreateMap<CreateCourseBindingModel, Course>();


            //Lecture Map
            CreateMap<Lecture, CourseLecturesViewModel>();
            CreateMap<Lecture, LectureViewModel>();
            CreateMap<AddLecturesBindingModel, Lecture>();


            //Instructor Map
            CreateMap<Course, CourseEditViewModel>();
        }
    }
}
