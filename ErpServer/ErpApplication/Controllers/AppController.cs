using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ErpApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ErpApplication.Controllers
{
    public class AppController : Controller
    {   
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult System()
        {
            ViewBag.Title = "System";
            return View();
        }
        [HttpGet("/about")]
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

    }
}