using Erp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Erp.Controllers
{
    /// <summary>
    /// This Controller Handles App Requests And will Contains the System Modules Actions
    /// </summary>
    [Authorize(Roles ="Employee")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class AppController : Controller
    {
        private IAuthenticationService _authorizationService;
        private IAuthenticationSchemeProvider _authentication;
        private IServiceProvider _service;
        private AccountDbContext mcontext;

        public AppController(IAuthenticationService authorizationService ,IAuthenticationSchemeProvider authentication ,IServiceProvider service, AccountDbContext context)
        {
            _authorizationService = authorizationService;
             _authentication = authentication;
            _service = service;
             mcontext = context;
        }
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public IActionResult Home()
        {
            if (User.IsInRole("Employee"))
                return RedirectToAction("System", "App");
           
            /*if (mcontext.Users.Any())
                return RedirectToAction("Login", "Account");*/
            else
                return RedirectToAction("Register", "Account");
            
        }

        [HttpGet("UserTask/{id}")]
        public IActionResult UserTask([FromRoute]string id)
        {

            ViewBag.Id = id;
            return View("UserTask");
        } 
        public IActionResult System()
        {
     
            ViewBag.CurrentBag = "system";
            ViewBag.Title = "System";       
            return View();
        }

        public IActionResult Modules()
        {
           
            return View();
        }

        public IActionResult YourUserTasks()
        {
            return View();
        }
    }
}