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

        public IEnumerable<LectureViewModel> GetLecturesByCourseIdAsync(int courseId, string userId, bool isAdmin)
        {
            var models = this.db.StudentCourses
                .Where(x => x.CourseId == courseId && x.StudentId == userId)
                .SelectMany(x => x.Course.Lectures)
                .To<LectureViewModel>()
                .ToList();

            if (models.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.UnauthorizedUser, userId));
            }
            else
            {
                return models;
            }
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        [RequestSizeLimit(1073741824)]
        public void Add(AddLecturesBindingModel model)
        {
            var lecture = new Lecture
            {
                CourseId = model.CourseId,
                Name = model.Name,
                Video = this.cloudinaryService.UploadVideo(model.Video)
            };

            this.db.Lectures.Add(lecture);
            this.db.SaveChanges();
        }

        public VideoViewModel GetVideoByLectureId(int lectureId, string userId)
        {
            var courseId = this.db.StudentCourses
                .Where(x => x.StudentId == userId)
                .SelectMany(x => x.Course.Lectures)
                .Where(x => x.Id == lectureId)
                .Select(x => x.Course.Id)
                .FirstOrDefault();

            if (courseId == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.UnauthorizedUser, userId));
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
