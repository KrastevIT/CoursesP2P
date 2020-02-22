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

namespace CoursesP2P.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public StudentsService(
            CoursesP2PDbContext db,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<CourseEnrolledViewModel>> GetMyCoursesAsync(ClaimsPrincipal user)
        {
            var student = await this.userManager.GetUserAsync(user);

            var courses = this.db.StudentCourses
                .Where(x => x.StudentId == student.Id)
                .Include(x => x.Course)
                .ThenInclude(x => x.Lectures)
                .Select(x => x.Course)
                .ToList();

            var models = this.mapper.Map<IEnumerable<CourseEnrolledViewModel>>(courses);

            return models;
        }

        public bool Add(int id, string studentId)
        {
            var course = this.db.Courses.Find(id);

            var isCreatedCourseFromCurrentInstructor = course.InstructorId == studentId;

            var existsCourse = this.db.StudentCourses
                .Where(x => x.StudentId == studentId)
                .ToList()
                .Any(x => x.CourseId == course.Id);

            if (existsCourse || isCreatedCourseFromCurrentInstructor)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = course.Id
            };

            var instructor = this.db.Users.FirstOrDefault(x => x.Id == course.InstructorId);
            instructor.Profit += course.Price;

            course.Orders++;

            this.db.Users.Update(instructor);

            this.db.StudentCourses.Add(studentCourse);

            this.db.SaveChanges();

            return true;
        }
    }
}
