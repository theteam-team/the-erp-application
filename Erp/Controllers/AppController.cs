using Erp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Erp.Controllers
{
    public class AppController : Controller
    {
        private AccountsDbContext mcontext;

        public AppController(AccountsDbContext context)
        {
            mcontext = context;
        }
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
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
            if (CommonNeeds.CurrentPath.Keys.Contains(User))
            {
                CommonNeeds.CurrentPath[User] = HttpContext.Request.Path;
            }
            else
                CommonNeeds.CurrentPath.Add(User, HttpContext.Request.Path);
            Console.WriteLine("Path" + " = " + CommonNeeds.CurrentPath[User]);
            ViewBag.Title = "System";
            return View();
        }
    }
}