using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Instructors
{
    public class Test
    {
        public bool Name { get; set; }
    }

    public class InstructorsService : IInstructorsService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IAzureStorageBlobService azureStorageBlobService;

        public InstructorsService(CoursesP2PDbContext db, IAzureStorageBlobService azureStorageBlobService)
        {
            this.db = db;
            this.azureStorageBlobService = azureStorageBlobService;
        }

        public IEnumerable<CourseInstructorViewModel> GetCreatedCourses(string userId)
        {
            var models = this.db.Courses
                .Where(x => x.InstructorId == userId)
                .To<CourseInstructorViewModel>()
                .ToList();
            models.ForEach(x => x.IsReview = this.db.Reviews.Any(y => y.CourseId == x.Id));

            return models;
        }

        public async Task EditCourseAsync(CourseEditViewModel model, string userId)
        {
            var course = await this.db.Courses.FirstOrDefaultAsync(x => x.Id == model.Id && x.InstructorId == userId);
            if (course == null)
            {
                throw new ArgumentNullException(
                    string.Format(ExceptionMessages.InvalidCourseId, model.Id));
            }

            if (model.ImageUpload != null)
            {
                model.Image = await this.azureStorageBlobService.UploadImageAsync(model.ImageUpload);
            }

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
