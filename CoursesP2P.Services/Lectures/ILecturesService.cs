﻿using CoursesP2P.Models;
using CoursesP2P.ViewModels.Lectures.BindingModels;
using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Lectures
{
    public interface ILecturesService
    {
        IEnumerable<LectureViewModel> GetLecturesByCourse(int courseId, string userId, bool isAdmin);

        Task AddAsync(AddLecturesBindingModel model);

        VideoViewModel GetVideoByLectureId(int lectureId, string userId, bool isAdmin);

        AddLecturesBindingModel GetLectureBindingModelWithCourseId(int courseId, User user);
    }
}
