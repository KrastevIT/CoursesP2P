using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.ViewModels.Courses.BindingModels;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;

        public CoursesService(CoursesP2PDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            var courses = this.db.Courses
                .Include(x => x.Lectures)
                .ToList();

            var models = this.mapper.Map<IEnumerable<CourseViewModel>>(courses);

            return models;
        }

        public CourseEditViewModel GetCourseById(int id)
        {
            var course = this.db.Courses
                .Include(x => x.Lectures)
                .FirstOrDefault(x => x.Id == id);

            var model = this.mapper.Map<CourseEditViewModel>(course);

            return model;
        }

        public IEnumerable<CourseViewModel> GetCoursesByCategory(string id)
        {
            var isValidEnum = Enum.TryParse(typeof(Category), id, true, out object category);
            if (!isValidEnum)
            {
                return null;
            }

            var coursesByCategory = this.db.Courses
                .Include(x => x.Lectures)
                .ToList()
                .Where(x => x.Category == (Category)category)
                .ToList();

            var models = this.mapper.Map<IEnumerable<CourseViewModel>>(coursesByCategory);

            return models;
        }

        public async Task Create(CreateCourseBindingModel model, User user)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images", fileName);

            bool exists = System.IO.Directory.Exists("wwwroot/Images");
            if (!exists)
            {
                Directory.CreateDirectory("wwwroot/Images");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var dbPath = "/Images/" + fileName;


            model.InstructorFullName = user.FirstName + ' ' + user.LastName;
            model.InstructorId = user.Id;

            var course = this.mapper.Map<Course>(model);
            course.Image = dbPath;

            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }

        public CourseDetailsViewModel Details(int id)
        {
            var course = this.db.Courses
                .Include(x => x.Lectures)
                .FirstOrDefault(x => x.Id == id);

            var lecturesName = course.Lectures.Select(x => x.Name).ToList();

            var splitSkills = course.Skills.Split('*')
                .Select(x => x.Trim())
                .Where(x => x != string.Empty)
                .ToList();

            var model = this.mapper.Map<CourseDetailsViewModel>(course);
            model.LectureName = lecturesName;
            model.Skills = splitSkills;

            return model;
        }

        public IEnumerable<CourseViewModel> Search(string searchTerm)
        {
            var foundCourses = this.db.Courses
              .Include(x => x.Lectures)
              .Where(x => x.Name.ToLower()
              .Contains(searchTerm.ToLower()))
              .ToList();

            var searchResult = this.mapper.Map<IEnumerable<CourseViewModel>>(foundCourses);

            return searchResult;
        }
    }
}
