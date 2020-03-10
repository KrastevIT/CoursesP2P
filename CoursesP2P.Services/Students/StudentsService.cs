using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Data.Migrations;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Courses.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private const decimal THIRTY_PERCENT = 0.30M;

        private readonly CoursesP2PDbContext db;
        private readonly IMapper mapper;

        public StudentsService(
            CoursesP2PDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<CourseEnrolledViewModel> GetMyCoursesAsync(User student)
        {
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

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = course.Id
            };

            var instructor = this.db.Users.FirstOrDefault(x => x.Id == course.InstructorId);
            instructor.Profit += course.Price * THIRTY_PERCENT;
            AddPaymentToInstructor(instructor);

            course.Orders++;

            this.db.Users.Update(instructor);

            this.db.StudentCourses.Add(studentCourse);

            this.db.SaveChanges();

            return true;
        }

        private void AddPaymentToInstructor(User instructor)
        {
            var payment = this.db.PaymentsToInstructors.FirstOrDefault(x => x.InstructorId == instructor.Id);
            if (payment == null)
            {
                var paymentsToInstructors = new PaymentToInstructor
                {
                    InstructorEmail = instructor.Email,
                    InstructorId = instructor.Id,
                    Amount = instructor.Profit
                };
                this.db.PaymentsToInstructors.Add(paymentsToInstructors);
            }
            else
            {
                payment.Amount = instructor.Profit;
                this.db.PaymentsToInstructors.Update(payment);
            }

            this.db.SaveChanges();
        }
    }
}
