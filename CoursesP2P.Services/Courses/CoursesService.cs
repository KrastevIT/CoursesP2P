using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IAzureStorageBlobService azureStorageBlobService;

        public CoursesService(
            CoursesP2PDbContext db,
            IAzureStorageBlobService azureStorageBlobService)
        {
            this.db = db;
            this.azureStorageBlobService = azureStorageBlobService;
        }

        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            return this.db.Courses.To<CourseViewModel>().ToList();
        }

        public IEnumerable<CourseViewModel> GetCoursesByCategory(string categoryName)
        {
            var isValidEnum = Enum.TryParse(typeof(Category), categoryName, true, out object category);
            if (isValidEnum)
            {
                return this.db.Courses
                    .Where(x => x.Category == (Category)category)
                    .To<CourseViewModel>()
                    .ToList();
            }

            throw new InvalidCastException(
                   string.Format(ExceptionMessages.InvalidCastCategory, categoryName));

        }

        public async Task CreateAsync(CreateCourseBindingModel model, string userId, string userFirstName, string userLastName)
        {
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = (Category)model.Category,
                Image = await this.azureStorageBlobService.UploadImageAsync(model.Image),
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
    }
}
