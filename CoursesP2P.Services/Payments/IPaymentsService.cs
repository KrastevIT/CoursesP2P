using CoursesP2P.Models;
using CoursesP2P.ViewModels.Admin;
using System.Collections.Generic;

namespace CoursesP2P.Services.Payments
{
    public interface IPaymentsService
    {
        string GetPayLink(int courseId, User student);

        int ProcessPayment(string paymentId, string payerId, string token);

        IEnumerable<AdminPayoutsViewModel> GetPayouts();

        void Payouts(IEnumerable<AdminPayoutsViewModel> models);
    }
}
