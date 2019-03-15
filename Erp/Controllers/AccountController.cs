using Erp.Data;
using Erp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Controllers
{  /// <summary>
   /// The Countroller That handels User login, logout and Registeration
   /// </summary>
    public class AccountController : Controller
    {
        private int x = 10;
        private ILogger<ApplicationUser> muserLogger; // This object used to implement the logging with respect to Users Activity.
        private Management _management;  // This object used to integrate the management system with this Controller.
        private DataDbContext mdataDbContext;   // This object Used as entry to the database created to store the system data with respect to a specific user.
        private AccountsDbContext mContext;   // this object used as an entry to the database creaded to store Accounts information.      
        private UserManager<ApplicationUser> mUserManager;  //used to manage the user stored in the database according to an API.
        private SignInManager<ApplicationUser> mSignInManager; //used To sign in the user according to an Api.
        public AccountController(AccountsDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<ApplicationUser> userlogger, Management management)
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
            x++;
            if (User.Identity.IsAuthenticated)  
            {

                if (HttpContext.Session.GetString("LastPageView") != null)
                    return Redirect(HttpContext.Session.GetString("LastPageView"));
                return RedirectToAction("System", "App"); 
            }
            ViewBag.CurrentView = "login";
            HttpContext.Session.SetString("LastPageView", HttpContext.Request.Path);
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
            HttpContext.Session.SetString("LastPageView", HttpContext.Request.Path);
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

                    await _management.AddRoleToUserAsync(roleName, user);

                    muserLogger.LogInformation("A user with a specifc roles : " + roleName + " has Been Created");

                    if (!CommonNeeds.checkdtb(mdataDbContext, registerModel.DataBaseName))
                    {
                        mdataDbContext.Database.EnsureCreated();
                    }
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
                ModelState.AddModelError("", "This Database Name is used");

            return View();

        }

        [HttpPost]
        [Authorize(Policy = "CreateUsers")]
        public async Task<IActionResult> CreateUser(IndexViewModel mod)
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

                await _management.AddRoleToUserAsync(registerModel.Role, client);

                muserLogger.LogInformation("A user with a specfic Role : "
                    + registerModel.Role + " Has been created by A user With A specifc Roles: ");

                var roles = await _management.GetUserRoleAsync(user);

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
            
            ModelState.AddModelError("", "You don't Have the Authority To Do that , please Contact The System Adminstrator");

            Console.WriteLine("\n" + HttpContext.Session.GetString("LastPageView"));
            if(HttpContext.Session.GetString("LastPageView") != null)
                return Redirect(HttpContext.Session.GetString("LastPageView"));

            return RedirectToAction("App", "System");
            
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