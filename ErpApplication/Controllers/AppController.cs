using ErpApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ErpApplication.Controllers
{
    public class AppController : Controller
    {
        private AccountsDbContext mcontext;

        public AppController(AccountsDbContext context)
        {
            mcontext = context;
        }
        [HttpGet("/")]
        public IActionResult Home()
        {
            if (mcontext.Users.Any())
                return RedirectToAction("Login", "Account");
            else
                return RedirectToAction("Register", "Account");
            
        }
        [Authorize]
        public IActionResult System()
        {
            CommonNeeds.CurrentPath = HttpContext.Request.Path;
            
            ViewBag.Title = "System";
            return View();
        }
        

    }
}