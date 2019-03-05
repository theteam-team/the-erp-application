using Erp.Data;
using Erp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    public class AccountController : Controller
    {

        private ILogger<AccountsDbContext> muserLogger;
        private RoleManager<ApplicationRole> mroleManager;
        private DataDbContext mdataDbContext;
        protected AccountsDbContext mContext;
        private UserManager<ApplicationUser> mUserManager;
        private SignInManager<ApplicationUser> mSignInManager;

        public IConfiguration Configuration { get; }

        public AccountController(AccountsDbContext context, DataDbContext dataDbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
            ILogger<AccountsDbContext> userlogger)
        {
            muserLogger = userlogger;
            mroleManager = roleManager;
            mdataDbContext = dataDbContext;
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
           
         

            if (this.User.Identity.IsAuthenticated)
            {
                return Redirect(CommonNeeds.CurrentPath[User]);
            }

            ViewBag.CurrentView = "login";

            if (CommonNeeds.CurrentPath.Keys.Contains(User))
            {
                CommonNeeds.CurrentPath[User] = HttpContext.Request.Path;
            }
            else
                CommonNeeds.CurrentPath.Add(User, HttpContext.Request.Path);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IndexViewModel mod)
        {

            LoginModel signInModel = mod.LoginModel;

            var user = mContext.Admins.Where(dt => dt.UserName == signInModel.UserName).First();

            if (user != null && signInModel.DatabaseName == user.DatabaseName)
            {
                var result = await mSignInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,
                        true, false);
                if (result.Succeeded)
                {
                    CommonNeeds.checkdtb(mdataDbContext, signInModel.DatabaseName);
                    var roles = await mUserManager.GetRolesAsync(user);

                    muserLogger.LogInformation("A user with a specifc roles : ");
                    foreach (var el in roles)
                    {
                        Console.Write(" " + el);
                    }
                    Console.Write(" has logged int the system");

                    return RedirectToAction("System", "App");
                }
            }
            ModelState.AddModelError("", "Wrong Entry");

            return View();

        }

        [HttpGet]
        public IActionResult Register()
        {
            if (CommonNeeds.CurrentPath.Keys.Contains(User))
            {
                CommonNeeds.CurrentPath[User] = HttpContext.Request.Path;
            }
            else
                CommonNeeds.CurrentPath.Add(User, HttpContext.Request.Path);
            ViewBag.CurrentView = "register";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexViewModel mod)
        {
            Register registerModel = mod.Register;

            var database = mContext.Admins.Where(dt => dt.DatabaseName == registerModel.DataBaseName);

            if (database != null)
            {

                var user = new ApplicationUser
                {
                    Email = registerModel.Email,
                    DatabaseName = registerModel.DataBaseName,
                    Country = registerModel.Country,
                    Language = registerModel.Language,
                    UserName = registerModel.UserName

                };
                var result = await mUserManager.CreateAsync(user, registerModel.Password);


                if (result.Succeeded)
                {


                    var roleName = "Adminstrator";



                    var role = await mroleManager.RoleExistsAsync(roleName);
                    if (!role)
                    {
                        await mroleManager.CreateAsync(new ApplicationRole(roleName));
                    }

                    await mUserManager.AddToRoleAsync(user, roleName);

                    muserLogger.LogInformation("A user with a specifc roles : " + roleName + " has Been Created");

                    CommonNeeds.checkdtb(mdataDbContext, registerModel.DataBaseName);

                    IndexViewModel viewModel = new IndexViewModel
                    {
                        Register = registerModel,
                        LoginModel = new LoginModel
                        {
                            DatabaseName = registerModel.DataBaseName,
                            UserName = registerModel.UserName,
                            Password = registerModel.Password
                        }

                    };


                    var res = await mSignInManager.PasswordSignInAsync(user.UserName, registerModel.Password,
                        true, false);
                    if (res.Succeeded)
                    {

                        return RedirectToAction("System", "App");
                    }

                }

                var errors = result.Errors.ToList();
                foreach (var el in errors)
                {
                    ModelState.AddModelError("", el.Code);
                }
            }
            else
                ModelState.AddModelError("", "This Database name is used");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register_AUser(IndexViewModel mod)
        {


            Register registerModel = mod.Register;

            var user = await mUserManager.GetUserAsync(User);

            var client = (new ApplicationUser
            {
                Email = registerModel.Email,
                DatabaseName = user.DatabaseName,
                Country = registerModel.Country,
                Language = registerModel.Language,
                UserName = registerModel.UserName

            });
            var result = await mUserManager.CreateAsync(client, registerModel.Password);
            if (result.Succeeded)
            {
                var role = await mroleManager.RoleExistsAsync(registerModel.Role);
                if (!role)
                {
                    await mroleManager.CreateAsync(new ApplicationRole(registerModel.Role));
                }

                await mUserManager.AddToRoleAsync(client, registerModel.Role);
                muserLogger.LogInformation("A user with a specfic Role : "
                    + registerModel.Role + " Has been created by A user With A specifc Roles: ");

                var roles = await mUserManager.GetRolesAsync(user);
                foreach (var el in roles)
                {
                    Console.Write(" " + el);
                }
            }
            var errors = result.Errors.ToList();

            foreach (var el in errors)
            {
                ModelState.AddModelError("", el.Code);
            }
            return Redirect(CommonNeeds.CurrentPath[User]);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await mSignInManager.SignOutAsync();
            muserLogger.LogInformation("A user With specific Roles : ");
            var user = await mUserManager.GetUserAsync(User);
            var roles = await mUserManager.GetRolesAsync(user);
            foreach (var el in roles)
            {
                Console.Write(" " + el);
            }
            Console.Write("Has been Logged out of the System");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult z3eem()
        {
            ViewBag.CurrentPath = HttpContext.Request.Path;
            ViewBag.CurrentView = "z3eem";
            return View();
        }

    }
}