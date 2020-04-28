using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Instructors
{
    public class InstructorsService : IInstructorsService
    {
        private readonly CoursesP2PDbContext db;

        public InstructorsService(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseInstructorViewModel> GetCreatedCourses(string userId)
        {
            var models = this.db.Courses
                .Where(x => x.InstructorId == userId)
                .To<CourseInstructorViewModel>()
                .ToList();

            return models;
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

        public CourseEditViewModel GetCourseById(int id, string userId)
        {
            var model = this.db.Courses
                    .Where(x => x.Id == id && x.InstructorId == userId)
                    .To<CourseEditViewModel>()
                    .FirstOrDefault();

            if (model == null)
            {
                throw new ArgumentNullException(
                    string.Format(ErrorMessages.NotFoundCourseById, id));
            }
            else
            {
                return model;
            }
        }
    }
}
