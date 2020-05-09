using CoursesP2P.Data;
using CoursesP2P.Models;
using System;
using System.Collections.Generic;
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
