using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.ViewModels.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesP2P.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly CoursesP2PDbContext db;

        public ReviewService(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public ReviewBindingModel GetReviewBindingModelWithCourseId(int courseId, string userId)
        {
            var isValid = this.db.Courses.Where(x => x.Id == courseId).FirstOrDefault()?.InstructorId == userId;
            var isReview = this.db.Reviews.Select(x => x.CourseId == courseId).First();
            if (!isValid || isReview)
            {
                return null;
            }
            var model = new ReviewBindingModel
            {
                CourseId = courseId
            };

            return model;
        }

        public async Task<bool> SaveReviewDbAsync(int courseId, string asset, string videoUrl)
        {
            var review = new Review
            {
                CourseId = courseId,
                VideoUrl = videoUrl,
                Asset = asset,
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
