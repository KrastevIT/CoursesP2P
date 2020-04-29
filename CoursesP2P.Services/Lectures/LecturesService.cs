using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Lectures
{
    public class LecturesService : ILecturesService
    {
        private readonly CoursesP2PDbContext db;
        private readonly ICloudinaryService cloudinaryService;

        public LecturesService(CoursesP2PDbContext db, ICloudinaryService cloudinaryService)
        {
            this.db = db;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<LectureViewModel> GetLecturesByCourse(int courseId, string userId, bool isAdmin)
        {
            var models = this.db.StudentCourses
                .Where(x => x.CourseId == courseId && x.StudentId == userId
                || isAdmin && x.CourseId == courseId)
                .SelectMany(x => x.Course.Lectures)
                .To<LectureViewModel>()
                .ToList();

            if (models.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.UnauthorizedUser, userId));
            }
            else
            {
                return models;
            }
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        [RequestSizeLimit(1073741824)]
        public async Task AddAsync(AddLecturesBindingModel model)
        {
            var lecture = new Lecture
            {
                CourseId = model.CourseId,
                Name = model.Name,
                Video = this.cloudinaryService.UploadVideo(model.Video)
            };

            await this.db.Lectures.AddAsync(lecture);
            await this.db.SaveChangesAsync();
        }

        public VideoViewModel GetVideoByLectureId(int lectureId, string userId, bool isAdmin)
        {
            var courseId = this.db.StudentCourses
                .Where(x => x.StudentId == userId || isAdmin)
                .SelectMany(x => x.Course.Lectures)
                .Where(x => x.Id == lectureId)
                .Select(x => x.Course.Id)
                .FirstOrDefault();

            if (courseId == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.UnauthorizedUser, userId));
            }
            else
            {
                var model = this.db.Lectures
                .Where(x => x.Id == lectureId)
                .To<VideoViewModel>()
                .FirstOrDefault();
                model.Lectures = this.db.Lectures
                    .Where(x => x.CourseId == courseId)
                    .To<VideoLectureViewModel>()
                    .ToList();

                return model;
            }
        }

        public AddLecturesBindingModel GetLectureBindingModelWithCourseId(int courseId, User user)
        {
            var isValid = this.db.Courses.Where(x => x.Id == courseId).FirstOrDefault()?.InstructorId == user.Id;
            if (!isValid)
            {
                return null;
            }
            var model = new AddLecturesBindingModel
            {
                CourseId = courseId
            };

            return model;
        }
    }
}
