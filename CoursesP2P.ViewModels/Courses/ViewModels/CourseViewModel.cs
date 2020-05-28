using AutoMapper;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName =>
           this.Name?.Length > 29
           ? this.Name.Substring(0, 29) + "..."
           : this.Name;

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public string InstructorFullName { get; set; }

        public int Orders { get; set; }

        public bool Status { get; set; }
        
        public ICollection<CourseLecturesViewModel> Lectures { get; set; }

        public int? Ratings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(x => x.Ratings, y => y.MapFrom(x => x.Ratings.Count));
        }
    }
}
