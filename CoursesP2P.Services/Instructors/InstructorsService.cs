using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Instructors
{
    public class InstructorsService : IInstructorsService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;

        public InstructorsService(
            CoursesP2PDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public CourseAndDashbordViewModel GetCreatedCourses(User instructor)
        {
            var courses = this.db.Courses
                 .Where(x => x.InstructorId == instructor.Id)
                 .Include(x => x.Lectures)
                 .Include(x => x.Students)
                 .ToList();

            var modelsCourse = this.mapper.Map<IEnumerable<CourseInstructorViewModel>>(courses);

            var model = new CourseAndDashbordViewModel
            {
                Courses = modelsCourse,
                CreatedCourses = courses.Count(),
                EnrolledCourses = courses.Select(x => x.Students).Sum(x => x.Count),
                Profit = instructor.Profit
            };

            return model;
        }

        public void EditCourse(CourseEditViewModel model)
        {
            var course = this.db.Courses.FirstOrDefault(x => x.Id == model.Id);
            if (course == null)
            {
                return;
            }
            model.Image = course.Image;

            this.db.Entry(course)
                 .CurrentValues.SetValues(model);

            this.db.SaveChanges();
        }
    }
}
