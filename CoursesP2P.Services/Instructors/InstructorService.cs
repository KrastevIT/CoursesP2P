using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Instructors
{
    public class InstructorService : IInstructorService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public InstructorService(
            CoursesP2PDbContext db,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<CourseAndDashbordViewModel> GetCreatedCourses(ClaimsPrincipal user)
        {
            var instructor = await this.userManager.GetUserAsync(user);

            var courses = this.db.Courses
                 .Where(x => x.InstructorId == instructor.Id)
                 .Include(x => x.Lectures)
                 .Include(x => x.Students)
                 .ToList();

            var modelsCourse = this.mapper.Map<IEnumerable<CourseInstructorViewModel>>(courses);

            var model = new CourseAndDashbordViewModel
            {
                Courses = modelsCourse,
                CreatedCourses = courses.Count(),
                EnrolledCourses = courses.Select(x => x.Students).Sum(x => x.Count),
                Profit = instructor.Profit
            };

            return model;
        }

        public void EditCourse(CourseEditViewModel model)
        {
            var course = this.db.Courses.FirstOrDefault(x => x.Id == model.Id);
            if (course == null)
            {
                return;
            }
            model.Image = course.Image;

            this.db.Entry(course)
                 .CurrentValues.SetValues(model);

            this.db.SaveChanges();
        }
    }
}
