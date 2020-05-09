using System.Threading.Tasks;

namespace CoursesP2P.Services.Reviews
{
    public interface IReviewService
    {
        Task<bool> SaveReviewDbAsync(int courseId, string asset, string videoUrl);
    }
}
