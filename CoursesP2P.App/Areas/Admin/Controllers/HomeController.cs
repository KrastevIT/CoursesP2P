﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{
   
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}