using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly CoursesP2PDbContext db;

        public CoursesService(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseViewModel> GetАpprovedCourses()
        {
            return this.db.Courses.Where(x => x.Status).To<CourseViewModel>().ToList();
        }

        public IEnumerable<CourseViewModel> GetCoursesByCategory(string categoryName, int? take = null, int skip = 0)
        {
            var isValidEnum = Enum.TryParse(typeof(Category), categoryName, true, out object category);
            if (isValidEnum)
            {
                var models = this.db.Courses
                    .Where(x => x.Category == (Category)category && x.Status)
                    .Skip(skip);
                if (take.HasValue)
                {
                    models = models.Take(take.Value);
                }

                return models.To<CourseViewModel>().ToList();
            }

            throw new InvalidCastException(
                   string.Format(ExceptionMessages.InvalidCastCategory, categoryName));

        }

        public CategoryViewModel GetCategoryDetails(string name, int page)
        {
            var isValidEnum = Enum.TryParse(typeof(Category), name, true, out object category);
            var count = this.db.Courses.Where(x => x.Status && x.Category == (Category)category).Count();
            var categoryDetails = new CategoryViewModel
            {
                Name = name,
                PagesCount = (int)Math.Ceiling((double)count / 6),
                CurrentPage = page
            };

            return categoryDetails;
        }

        public async Task CreateAsync(CreateCourseBindingModel model, string userId, string userFirstName, string userLastName, string imageUrl)
        {
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = model.Category,
                Image = imageUrl,
                Skills = model.Skills,
                CreatedOn = DateTime.UtcNow,
                InstructorFullName = userFirstName + ' ' + userLastName,
                InstructorId = userId
            };

            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();
        }

        public CourseDetailsViewModel Details(int id)
        {
            var model = this.db.Courses
                .Where(x => x.Id == id)
                .To<CourseDetailsViewModel>()
                .FirstOrDefault();

            if (model == null)
            {
                throw new ArgumentNullException(
                    string.Format(ExceptionMessages.NotFoundCourseById, id));
            }

            model.Skills = this.db.Courses
                .Where(x => x.Id == id)
                .Select(x => x.Skills)
                .FirstOrDefault()?
                .Split('*')
                .Select(x => x.Trim())
                .ToList();

            model.LectureName = this.db.Lectures
                .Where(x => x.CourseId == id)
                .Select(x => x.Name)
                .ToList();

            model.Video = this.db.Reviews
                .Where(x => x.CourseId == id)
                .Select(x => x.VideoUrl)
                .FirstOrDefault();

            return model;
        }

        public IEnumerable<CourseViewModel> Search(string searchTerm)
        {
            return this.db.Courses
              .Where(x => x.Name.ToLower()
              .Contains(searchTerm.ToLower()))
              .To<CourseViewModel>()
              .ToList();
        }

        public IEnumerable<CourseViewModel> GetWaitingCourses()
        {
            return this.db.Courses
                .Where(x => x.Status == false && x.Active)
                .To<CourseViewModel>()
                .ToList();
        }
    }
}
