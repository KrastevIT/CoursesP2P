using AutoMapper;
using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
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

        public IEnumerable<CourseInstructorViewModel> GetCreatedCourses(User instructor)
        {
            var courses = this.db.Users
                .Where(x => x.Id == instructor.Id)
                .SelectMany(x => x.CreatedCourses)
                .Include(x => x.Lectures)
                .ToList();

            var modelsCourse = this.mapper.Map<IEnumerable<CourseInstructorViewModel>>(courses);

            return modelsCourse;
        }

        public void EditCourse(CourseEditViewModel model)
        {
            var course = this.db.Courses.FirstOrDefault(x => x.Id == model.Id);
            if (course == null)
            {
                throw new ArgumentNullException(
                    string.Format(ErrorMessages.InvalidCourseId, model.Id));
            }
            model.Image = course.Image;

            this.db.Entry(course)
                 .CurrentValues.SetValues(model);

            this.db.SaveChanges();
        }
    }
}
