using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ErpApplication.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("/Account/SignIn")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("/Account/SignUp")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("/Account/SignIn"),ValidateAntiForgeryToken]
        public IActionResult Login(SignInModel model)
        {
           
               return RedirectToAction("System", "App");
            
            

                //return View();
            
            
        }
        [HttpPost("/Account/SignUp"), ValidateAntiForgeryToken]
        public IActionResult Register(Register model)
        {
            
              return  RedirectToAction("System", "App");
            

            //return View();

        }

    }
}