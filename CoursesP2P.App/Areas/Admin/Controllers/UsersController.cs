﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}