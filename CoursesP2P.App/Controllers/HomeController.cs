﻿using AutoMapper;
using CoursesP2P.App.Models;
using CoursesP2P.App.Models.ViewModels.Course;
using CoursesP2P.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CoursesP2P.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly CoursesP2PDbContext coursesP2PDbContext;
        private readonly IMapper mapper;

        public HomeController(
            ILogger<HomeController> logger,
            CoursesP2PDbContext coursesP2PDbContext,
            IMapper mapper)
        {
            this.logger = logger;
            this.coursesP2PDbContext = coursesP2PDbContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var courses = this.coursesP2PDbContext.Courses
                .Include(x => x.Lectures)
                .ToList();

            var models = new List<CourseViewModel>();

            courses.ForEach(course => models.Add(this.mapper.Map<CourseViewModel>(course)));

            return View(models);
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            var searchResult = new List<CourseViewModel>();

            var foundCourses = this.coursesP2PDbContext.Courses
               .Include(x => x.Lectures)
               .Where(x => x.Name.ToLower()
               .Contains(searchTerm.ToLower()))
               .ToList();

            foundCourses.ForEach(course => searchResult.Add(this.mapper.Map<CourseViewModel>(course)));

            return View(searchResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
