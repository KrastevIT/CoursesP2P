using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return this.db.Courses
                .Where(x => x.InstructorId == userId)
                .To<CourseInstructorViewModel>()
                .ToList();
        }

        public async Task EditCourseAsync(CourseEditViewModel model)
        {
            var course = await this.db.Courses.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (course == null)
            {
                throw new ArgumentNullException(
                    string.Format(ExceptionMessages.InvalidCourseId, model.Id));
            }
            model.Image = course.Image;

            this.db.Entry(course)
                 .CurrentValues.SetValues(model);

            await this.db.SaveChangesAsync();
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
                    string.Format(ExceptionMessages.NotFoundCourseById, id));
            }
            else
            {
                return model;
            }
        }
    }
}
