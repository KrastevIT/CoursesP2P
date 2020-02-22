using AutoMapper;
using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoursesP2P.Services.Lectures
{
    public class LecturesService : ILecturesService
    {
        //TODO
        private const string PathLecturesVideos = "wwwroot/Videos";

        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;

        public LecturesService(
            CoursesP2PDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<LectureViewModel> GetLecturesByCourseIdAsync(int id, User instructor)
        {
            var user = this.db.Users
                .Include(x => x.EnrolledCourses)
                .FirstOrDefault(x => x.Id == instructor.Id);

            var isValidUser = user.EnrolledCourses.Any(x => x.CourseId == id);
            if (!isValidUser)
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.UnauthorizedUser, instructor.UserName));
            }
            var lectures = this.db.Lectures
                .Where(x => x.CourseId == id)
                .ToList();

            var models = this.mapper.Map<IEnumerable<LectureViewModel>>(lectures);

            return models;
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 1000000000)]
        [RequestSizeLimit(1000000000)]
        public void Add(AddLecturesBindingModel model)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Video.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Videos", fileName);

            bool exists = System.IO.Directory.Exists("wwwroot/Videos");
            if (!exists)
            {
                Directory.CreateDirectory("wwwroot/Videos");
            }

            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                model.Video.CopyTo(fileStream);
                fileStream.Flush();
            }

            var dbPath = "/Videos/" + fileName;

            var lecture = this.mapper.Map<Lecture>(model);
            lecture.Video = dbPath;

            this.db.Lectures.Add(lecture);
            this.db.SaveChanges();
        }

        public VideoViewModel GetVideoByLectureId(int id)
        {
            var lecture = this.db.Lectures.FirstOrDefault(x => x.Id == id);
            if (lecture == null)
            {
                throw new ArgumentNullException(
                    string.Format(ErrorMessages.InvalidLectureId, id));
            }
            var lecturesOfCourse = this.db.Lectures
                .Where(x => x.CourseId == lecture.CourseId)
                .ToList();

            var modelVideos = this.mapper.Map<IEnumerable<VideoLectureViewModel>>(lecturesOfCourse);

            var model = this.mapper.Map<VideoViewModel>(lecture);
            model.Lectures = modelVideos;

            return model;
        }
    }
}
