using CoursesP2P.Models;

namespace CoursesP2P.Services.Payments
{
    public interface IPaymentsService
    {
        string GetPayLink(int courseId, User student);

        void ProcessPayment(string paymentId, string payerId, string token);
    }
}
