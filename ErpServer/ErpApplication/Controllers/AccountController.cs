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
        public IActionResult Login(IndexViewModel mod)
        {
            SignInModel signInModel = new SignInModel();
            signInModel = mod.SignInModel;
            if (signInModel == null)
            {
               Register registerModel = new Register();
               registerModel = mod.Register;
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("System", "App");
            }
            else
            {
                return View();
            }
        }
        

    }
}