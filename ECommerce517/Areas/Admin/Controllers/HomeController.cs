﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce517.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class HomeController : Controller
    {
        [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminArea}")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
