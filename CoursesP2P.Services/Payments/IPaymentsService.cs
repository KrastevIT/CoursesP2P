namespace CoursesP2P.Services.Payments
{
    public interface IPaymentsService
    {
        string GetPayLink();

        void ProcessPayment(string paymentId, string payerId, string token);
    }
}
