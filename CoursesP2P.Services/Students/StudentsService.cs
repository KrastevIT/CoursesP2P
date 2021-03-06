﻿using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Mapping;
using CoursesP2P.ViewModels.Courses.ViewModels;
using CoursesP2P.ViewModels.FiveStars;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private const decimal THIRTY_PERCENT = 0.30M;

        private readonly CoursesP2PDbContext db;

        public StudentsService(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseEnrolledViewModel> GetMyCourses(string userId)
        {
            var models = this.db.StudentCourses
                .Where(x => x.StudentId == userId)
                .Select(x => x.Course)
                .To<CourseEnrolledViewModel>()
                .ToList();

            return models;
        }

        public async Task<bool> AddAsync(int id, string studentId)
        {
            var course = this.db.Courses.Find(id);

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = course.Id
            };

            var instructor = this.db.Users.FirstOrDefault(x => x.Id == course.InstructorId);
            instructor.Profit += course.Price * THIRTY_PERCENT;
            AddPaymentToInstructor(instructor, course.Price);

            course.Orders++;

           await this.db.StudentCourses.AddAsync(studentCourse);

           await this.db.SaveChangesAsync();

            return true;
        }

        private void AddPaymentToInstructor(User instructor, decimal price)
        {
            var payment = this.db.PaymentsToInstructors.FirstOrDefault(x => x.InstructorId == instructor.Id);
            if (payment == null)
            {
                payment = new PaymentToInstructor
                {
                    InstructorEmail = instructor.Email,
                    InstructorId = instructor.Id,
                    Amount = price * THIRTY_PERCENT
                };
                this.db.PaymentsToInstructors.Add(payment);
            }
            else
            {
                payment.Amount += price * THIRTY_PERCENT;
                this.db.PaymentsToInstructors.Update(payment);
            }
            this.db.SaveChanges();
        }

        public void AddRating(RatingViewModel model)
        {
            var rating = this.db.Ratings.FirstOrDefault(x => x.StudentId == model.StudentId && x.CourseId == model.CourseId);
            if (rating == null)
            {
                rating = new Rating
                {
                    StudentId = model.StudentId,
                    CourseId = model.CourseId,
                    Vote = model.Rating
                };
                this.db.Ratings.Add(rating);
            }
            else
            {
                rating.Vote = model.Rating;
                this.db.Ratings.Update(rating);
            }
            this.db.SaveChanges();
        }

        public RatingViewModel GetRating(string studentId, int courseId)
        {
            var model = new RatingViewModel
            {
                StudentId = studentId
            };
            var rating = this.db.Ratings.FirstOrDefault(x => x.StudentId == studentId && x.CourseId == courseId);
            if (rating == null)
            {
                model.Rating = 0;
                return model;
            }
            model.Rating = rating.Vote;

            return model;
        }
    }
}
