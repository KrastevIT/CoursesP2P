﻿using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public interface ICoursesService
    {
        IEnumerable<CourseViewModel> GetAllCourses();

        CourseEditViewModel GetCourseById(int id);

        IEnumerable<CourseViewModel> GetCoursesByCategory(string id);

        Task CreateAsync(CreateCourseBindingModel model, ClaimsPrincipal user);

        CourseDetailsViewModel Details(int id);

        IEnumerable<CourseViewModel> Search(string searchTerm);
    }
}
