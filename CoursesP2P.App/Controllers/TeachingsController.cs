﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoursesP2P.App.Controllers
{
    public class TeachingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}