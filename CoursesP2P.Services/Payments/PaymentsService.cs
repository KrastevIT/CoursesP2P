using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.ViewModels.PayPal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using PayPal.Api;
using System;
using System.Linq;

namespace CoursesP2P.Services.Payments
{
    public class PaymentsService : IPaymentsService
    {
        private readonly CoursesP2PDbContext db;
        private readonly PayPalSettings settings;

        public PaymentsService(CoursesP2PDbContext db, IOptions<PayPalSettings> settings)
        {
            this.db = db;
            this.settings = settings.Value;
        }

        public string GetPayLink(int courseId, User student)
        {
            var course = this.db.Courses.FirstOrDefault(x => x.Id == courseId);
            if (course == null)
            {
                //TODO Exception or retunr null
            }

            var token = new OAuthTokenCredential(this.settings.ClientId, this.settings.ClientSecret)
                .GetAccessToken();

            var apiContext = new APIContext(token);

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var amount = new Amount()
            {
                currency = "EUR",
                total = course.Price.ToString()
            };

            var transactions = new Transaction()
            {
                amount = amount,
            };

            var payment = new PayPal.Api.Payment()
            {
                payer = payer,
                transactions = new[] { transactions }.ToList(),
                intent = "sale",
                redirect_urls = new RedirectUrls()
                {
                    return_url = "https://localhost:44391/Payments/Process",
                    cancel_url = "https://localhost:44391/Payments/Cancel"
                }
            };

            var createdPayment = payment.Create(apiContext);
            var links = createdPayment.links.ToList();
            var approvalLink = links.FirstOrDefault(l => l.rel == "approval_url");

            var payModel = new Models.Payment
            {
                PaymentId = createdPayment.id,
                StudentEmail = student.Email,
                StudentId = student.Id,
                Amount = course.Price
            };

            this.db.Payments.Add(payModel);
            this.db.SaveChanges();

            return approvalLink.href;
        }

        public void ProcessPayment(string paymentId, string payerId, string token)
        {
            var apiToken = new OAuthTokenCredential(settings.ClientId, settings.ClientSecret)
                .GetAccessToken();

            var apiContext = new APIContext(apiToken);

            var payment = new PayPal.Api.Payment() { id = paymentId, token = token };

            var executed = payment.Execute(apiContext, new PaymentExecution() { payer_id = payerId });

            var dbPayment = this.db.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
            dbPayment.Status = PaymentStatus.Successfully;

            this.db.Payments.Update(dbPayment);
            this.db.SaveChanges();
        }
    }
}
