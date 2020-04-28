﻿using AutoMapper;
using Courses.P2P.Common;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Cloudinary;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Lectures
{
    public class LecturesService : ILecturesService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;

        public LecturesService(
            CoursesP2PDbContext db,
            IMapper mapper,
            ICloudinaryService cloudinaryService)
        {
            this.db = db;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<LectureViewModel> GetLecturesByCourseIdAsync(int id, string userId, bool isAdmin)
        {
            var models = this.db.Lectures
                .Where(x => x.Course.Students.Select(y => y.StudentId == userId).FirstOrDefault()
                && x.CourseId == id)
                .To<LectureViewModel>()
                .ToList();

            if (models.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.UnauthorizedUser, userId));
            }

            return models;
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

        public VideoViewModel GetVideoByLectureId(int id, User user)
        {
            var lecture = this.db.Lectures.FirstOrDefault(x => x.Id == id);
            var isValidUser = this.db.Courses
                .Where(x => x.Id == lecture.CourseId)
                .SelectMany(x => x.Students).Select(x => x.StudentId == user.Id)
                .FirstOrDefault();

            if (lecture == null)
            {
                throw new ArgumentNullException(
                    string.Format(ErrorMessages.InvalidLectureId, id));
            }
            else if (!isValidUser)
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.UnauthorizedUser, user.Id));
            }
            var lecturesOfCourse = this.db.Lectures
                .Where(x => x.CourseId == lecture.CourseId)
                .ToList();

            var modelVideos = this.mapper.Map<IEnumerable<VideoLectureViewModel>>(lecturesOfCourse);

            var model = this.mapper.Map<VideoViewModel>(lecture);
            model.Lectures = modelVideos;

            return model;
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
