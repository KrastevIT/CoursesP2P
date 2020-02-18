using CoursesP2P.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{

    public class HomeController : AdminController
    {
        private readonly IAdminService adminService;

        public HomeController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Index()
        {
            var users = this.adminService.GetUsers();

            return View(users);
        }
    }
}