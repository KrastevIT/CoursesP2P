using AutoMapper;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;

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
            CreateMap<Lecture, VideoLectureViewModel>();
            CreateMap<Lecture, VideoViewModel>();
            CreateMap<AddLecturesBindingModel, Lecture>();


            //Instructor Map
            CreateMap<Course, CourseEditViewModel>();

            //Admin Map
            CreateMap<User, AdminUserViewModel>();
            CreateMap<StudentCourse, AdminEnrollmentViewModel>();
        }
    }
}
