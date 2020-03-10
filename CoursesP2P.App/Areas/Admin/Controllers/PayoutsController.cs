using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.Services.Payments;
using CoursesP2P.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{
    public class PayoutsController : AdminController
    {
        private readonly IPaymentsService paymentsService;

        public PayoutsController(IPaymentsService paymentsService)
        {
            this.paymentsService = paymentsService;
        }

        public IActionResult Index()
        {
            var payouts = this.paymentsService.GetPayouts();

            return View(payouts);
        }


        public IActionResult Payout()
        {
            var models = this.paymentsService.GetPayouts();
            this.paymentsService.Payouts(models);

            return RedirectToAction("Index");
        }
    }
}