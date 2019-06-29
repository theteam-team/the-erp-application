using Erp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
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
            if (User.IsInRole("Employee"))
                return RedirectToAction("System", "App");
           
            if (mcontext.Users.Any())
                return RedirectToAction("Login", "Account");
            else
                return RedirectToAction("Register", "Account");
            
        }
        [Authorize(Roles ="Employee")]
        public IActionResult System()
        {
            //HttpContext.Session.SetString("LastPageView", HttpContext.Request.Path);          
            ViewBag.CurrentBag = "system";
            ViewBag.Title = "System";
            
            return View();
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Modules()
        {
           
            return View();
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Notification()
        {
            return View();
        }
    }
}