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
        private Management _management;
        private DataDbContext mdataDbContext;
        protected AccountsDbContext mContext;
        private UserManager<ApplicationUser> mUserManager;
        private SignInManager<ApplicationUser> mSignInManager;

        public IConfiguration Configuration { get; }

        public AccountController(AccountsDbContext context, DataDbContext dataDbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<AccountsDbContext> userlogger, Management management)
        {
            _management = management;
            muserLogger = userlogger;
            mdataDbContext = dataDbContext;
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
           
         

            if (this.User.Identity.IsAuthenticated  )
            {
                if(CommonNeeds.CurrentPath.Keys.Contains(User))
                    return Redirect(CommonNeeds.CurrentPath[User]);
                return RedirectToAction("System", "App");
                
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

            var user = await mUserManager.FindByNameAsync(signInModel.UserName);

            if (user != null && signInModel.DatabaseName == user.DatabaseName)
            {
                if (CommonNeeds.checkdtb(mdataDbContext, signInModel.DatabaseName))
                {
                    var result = await mSignInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,
                            true, false);
                    if (result.Succeeded)
                    {

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
                else
                    ModelState.AddModelError("", "database does not exist please contact system admin");
            }
            else           
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

                    await _management.AddRoleToUser(roleName, user);

                    muserLogger.LogInformation("A user with a specifc roles : " + roleName + " has Been Created");

                    if (!CommonNeeds.checkdtb(mdataDbContext, registerModel.DataBaseName))
                    {
                        mdataDbContext.Database.EnsureCreated();
                    }

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

                await _management.AddRoleToUser(registerModel.Role, client);
     
                muserLogger.LogInformation("A user with a specfic Role : "
                    + registerModel.Role + " Has been created by A user With A specifc Roles: ");

                var roles = await _management.GetUserRoleAsync(client);

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
            
            return RedirectToAction("System", "App");
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