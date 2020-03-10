using CoursesP2P.Data;
using CoursesP2P.ViewModels.PayPal;
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

        public string GetPayLink()
        {
            var token = new OAuthTokenCredential(settings.ClientId, settings.ClientSecret)
                .GetAccessToken();

            var apiContext = new APIContext(token);

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var amount = new Amount()
            {
                currency = "EUR",
                total = "10"
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
                    return_url = "https://localhost:44385/Payments/Process",
                    cancel_url = "https://localhost:44385/Payments/Cancel"
                }
            };

            var createdPayment = payment.Create(apiContext);
            var links = createdPayment.links.ToList();
            var approvalLink = links.FirstOrDefault(l => l.rel == "approval_url");


            return approvalLink.href;
        }

        public void ProcessPayment(string paymentId, string payerId, string token)
        {
            //var token = new OAuthTokenCredential(settings.ClientId, settings.ClientSecret)
            //    .GetAccessToken();

            //var apiContext = new APIContext(apiToken);

            //var payment = new PayPal.Api.Payment() { id = paymentId, token = token };

            //var executed = payment.Execute(apiContext, new PaymentExecution() { payer_id = payerId });

            //var modelPayment = new Models.Payment()
            //{
            //    PayPalPaymentId = paymentId,
            //    UserName = "Test",
            //    Amount = 10,
            //};

            //this.DbContext.Payments.Add(modelPayment);
            //this.DbContext.SaveChanges();
        }
    }
}
