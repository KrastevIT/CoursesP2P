﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{
    public class CoursesController : AdminController
    {
        private readonly IAdminService adminService;

        public CoursesController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult CreatedCourses(string id)
        {
            var courses = this.adminService.GetCreatedCoursesByUserId(id);
            return View(courses);
        }

        public IActionResult EnrolledCourses(string id)
        {
            var courses = this.adminService.GetEnrolledCoursesByUserId(id);
            return View(courses);
        }
    }
}