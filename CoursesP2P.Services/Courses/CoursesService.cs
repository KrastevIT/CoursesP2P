using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly CoursesP2PDbContext db;
        private readonly ICloudinaryService cloudinaryService;

        public CoursesService(
            CoursesP2PDbContext db,
            ICloudinaryService cloudinaryService)
        {
            this.db = db;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            var models = this.db.Courses.To<CourseViewModel>().ToList();
            return models;
        }

        public IEnumerable<CourseViewModel> GetCoursesByCategory(string categoryName)
        {
            var isValidEnum = Enum.TryParse(typeof(Category), categoryName, true, out object category);
            if (isValidEnum)
            {
                var models = this.db.Courses
                    .Where(x => x.Category == (Category)category)
                    .To<CourseViewModel>()
                    .ToList();

                return models;
            }

            throw new InvalidCastException(
                   string.Format(ErrorMessages.InvalidCastCategory, categoryName));

        }

        public async Task CreateAsync(CreateCourseBindingModel model, string userId, string userFirstName, string userLastName)
        {
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = (Category)model.Category,
                Image = await this.cloudinaryService.UploadImageAsync(model.Image),
                Skills = model.Skills,
                CreatedOn = DateTime.UtcNow,
                InstructorFullName = userFirstName + ' ' + userLastName,
                InstructorId = userId
            };

            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }

        public CourseDetailsViewModel Details(int id)
        {
            var course = this.db.Courses
                .Include(x => x.Lectures)
                .FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                throw new ArgumentNullException(
                    string.Format(ErrorMessages.NotFoundCourseById, id));
            }

            var lecturesName = course.Lectures.Select(x => x.Name).ToList();

            var splitSkills = course.Skills.Split('*')
                .Select(x => x.Trim())
                .Where(x => x != string.Empty)
                .ToList();

            //TODO Not Mapping exception
            var model = new CourseDetailsViewModel
            {
                Id = id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Skills = splitSkills,
                LectureName = lecturesName
            };

            return model;
        }

        public IEnumerable<CourseViewModel> Search(string searchTerm)
        {
            var searchResult = this.db.Courses
              .Where(x => x.Name.ToLower()
              .Contains(searchTerm.ToLower()))
              .To<CourseViewModel>()
              .ToList();

            return searchResult;
        }
    }
}
