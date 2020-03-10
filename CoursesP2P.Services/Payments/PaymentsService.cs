using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.ViewModels.Admin;
using CoursesP2P.ViewModels.PayPal;
using Microsoft.Extensions.Options;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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

            var isCreatedCourseFromCurrentInstructor = course.InstructorId == student.Id;

            var existsCourse = this.db.StudentCourses
                .Where(x => x.StudentId == student.Id)
                .ToList()
                .Any(x => x.CourseId == course.Id);

            if (existsCourse || isCreatedCourseFromCurrentInstructor)
            {
                return null;
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
                CourseId = course.Id,
                Amount = course.Price
            };

            this.db.Payments.Add(payModel);
            this.db.SaveChanges();

            return approvalLink.href;
        }

        public int ProcessPayment(string paymentId, string payerId, string token)
        {
            var apiToken = new OAuthTokenCredential(settings.ClientId, settings.ClientSecret)
                .GetAccessToken();

            var apiContext = new APIContext(apiToken);

            var payment = new PayPal.Api.Payment() { id = paymentId, token = token };

            var executed = payment.Execute(apiContext, new PaymentExecution() { payer_id = payerId });
            if (true)
            {
                //TODO VALIDATIONS
            }
            var dbPayment = this.db.Payments.FirstOrDefault(x => x.PaymentId == paymentId);
            dbPayment.Status = PaymentStatus.Successfully;

            this.db.Payments.Update(dbPayment);
            this.db.SaveChanges();

            return dbPayment.CourseId;
        }

        public IEnumerable<AdminPayoutsViewModel> GetPayouts()
        {
            return this.db.PaymentsToInstructors.Select(x => new AdminPayoutsViewModel
            {
                Email = x.InstructorEmail,
                Amount = x.Amount
            })
            .OrderByDescending(x => x.Amount)
            .ToList();
        }

        public void Payouts(IEnumerable<AdminPayoutsViewModel> models)
        {
            var apiToken = new OAuthTokenCredential(settings.ClientId, settings.ClientSecret)
                .GetAccessToken();

            var apiContext = new APIContext(apiToken);

            var payoutItems = new List<PayoutItem>();

            foreach (var model in models)
            {
                var payoutItem = new PayoutItem
                {
                    recipient_type = PayoutRecipientType.EMAIL,
                    amount = new Currency
                    {
                        value = model.Amount.ToString(),
                        currency = "EUR"
                    },
                    receiver = model.Email,
                    note = $"{model.Email} have a payment of  {model.Amount}",
                    sender_item_id = Guid.NewGuid().ToString()
                };

                payoutItems.Add(payoutItem);
            }

            var payout = new Payout
            {
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    sender_batch_id = Guid.NewGuid().ToString().Substring(0, 8),
                    email_subject = "You Have a Payment!"
                },
                items = payoutItems,
            };

            var createPayout = payout.Create(apiContext);
            var batchId = createPayout.batch_header.payout_batch_id;
            var getPayout = Payout.Get(apiContext, batchId);
            while (getPayout.batch_header.batch_status != "SUCCESS")
            {
                if (getPayout.batch_header.batch_status == "SUCCESS")
                {
                    var currentPayout = this.db.PaymentsToInstructors.ToList();
                    this.db.RemoveRange(currentPayout);
                }
            }
           

        }
    }
}
