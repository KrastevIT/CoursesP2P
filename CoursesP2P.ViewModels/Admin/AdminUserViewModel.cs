using AutoMapper;
using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.ViewModels.Admin
{
    public class AdminUserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<CourseViewModel> CreatedCourses { get; set; }

        public IEnumerable<AdminEnrollmentViewModel> EnrolledCourses { get; set; }

        public int Sales { get; set; }

        public decimal Profit { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, AdminUserViewModel>()
                 .ForMember(x => x.Sales, y =>
                 {
                     y.MapFrom(u => u.CreatedCourses.Select(x => x.Orders).Sum());
                 });

            configuration.CreateMap<User, AdminUserViewModel>()
                .ForMember(x => x.EnrolledCourses, y => y.MapFrom(x => x.EnrolledCourses));
        }
    }
}
