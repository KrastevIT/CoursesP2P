using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesP2P.Models;
using CoursesP2P.Services.Payments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentsService paymentsService;
        private readonly UserManager<User> userManager;

        public PaymentsController(IPaymentsService paymentsService, UserManager<User> userManager)
        {
            this.paymentsService = paymentsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var student = await this.userManager.GetUserAsync(this.User);
            string paypalLink = this.paymentsService.GetPayLink(id, student);
            if (paypalLink == null)
            {
                return BadRequest();
            }

            return Redirect(paypalLink);
        }


        public IActionResult Process(string paymentId, string payerId, string token)
        {
            this.paymentsService.ProcessPayment(paymentId, payerId, token);

            return RedirectToAction("Index", "Students");
        }

        public IActionResult Cancel()
        {
            return View("Index");
        }
    }
}