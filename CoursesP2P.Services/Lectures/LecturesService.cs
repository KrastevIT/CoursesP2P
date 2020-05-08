using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.AzureStorageBlob;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Lectures
{
    public class LecturesService : ILecturesService
    {
        private readonly CoursesP2PDbContext db;

        public IAzureStorageBlobService AzureStorageBlob { get; }

        public LecturesService(CoursesP2PDbContext db)
        {
            this.db = db;
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

        public async Task<bool> SaveLectureDbAsync(int courseId, string name,string asset ,string videoUrl)
        {
            var lecture = new Lecture
            {
                CourseId = courseId,
                Name = name,
                Video = videoUrl,
                Asset = asset
            };

            await this.db.Lectures.AddAsync(lecture);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
