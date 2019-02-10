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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(IndexViewModel model)
        {
            SignInModel signInModel = new SignInModel();
            signInModel = model.SignInModel;
            if (ModelState.IsValid)
            {
                return RedirectToAction("System", "App");
            }
            else
            {
                return View();
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(IndexViewModel model)
        {
            Register register = new Register();
            register = model.Register;
            if (ModelState.IsValid)
            {
                return RedirectToAction("System", "App");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

    }
}