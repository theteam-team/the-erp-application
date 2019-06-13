﻿using Erp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;

namespace Erp.Controllers
{
    /// <summary>
    /// This Controller Handles App Requests And will Contains the System Modules Actions
    /// </summary>
    public class AppController : Controller
    {
        private AccountDbContext mcontext;

        public AppController(AccountDbContext context)
        {
            mcontext = context;
        }
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("System", "App");

            if (mcontext.Users.Any())
                return RedirectToAction("Login", "Account");
            else
                return RedirectToAction("Register", "Account");
            
        }
        [Authorize]
        public IActionResult System()
        {
            HttpContext.Session.SetString("LastPageView", HttpContext.Request.Path);          
            ViewBag.CurrentBag = "system";
            ViewBag.Title = "System";
            
            return View();
        }
        public IActionResult Modules()
        {
           // ViewBag.CurrentPath = HttpContext.Request.Path;
           // ViewBag.CurrentView = "Modules";
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }
    }
}