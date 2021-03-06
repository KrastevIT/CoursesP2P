﻿using CoursesP2P.ViewModels.Courses.ViewModels;
using CoursesP2P.ViewModels.FiveStars;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Students
{
    public interface IStudentsService
    {
        IEnumerable<CourseEnrolledViewModel> GetMyCourses(string userId);

        Task<bool> AddAsync(int courseId, string studentId);

        void AddRating(RatingViewModel model);

        RatingViewModel GetRating(string studentId, int courseId);
    }
}
