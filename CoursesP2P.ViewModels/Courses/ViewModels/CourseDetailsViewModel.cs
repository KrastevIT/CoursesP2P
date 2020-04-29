using AutoMapper;
using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseDetailsViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [IgnoreMap]
        public ICollection<string> Skills { get; set; }
        [IgnoreMap]
        public ICollection<string> LectureName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration.CreateMap<Course, CourseDetailsViewModel>()
            //    .ForMember(x => x.Skills, y =>
            //    {
            //        y.MapFrom(c => c.Skills.Split("*", System.StringSplitOptions.None)
            //        .Select(x => x.Trim())
            //        .Where(x => x != string.Empty)
            //        .ToList());
            //    });

        }
    }
}
