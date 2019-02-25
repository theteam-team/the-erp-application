using ErpApplication.Data;
using ErpApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpApplication.Controllers
{
    public class AccountController : Controller
    {
        private RoleManager<ApplicationRole> mroleManager;
        private DataDbContext mdataDbContext;
        protected AccountsDbContext mContext;
        private UserManager<ApplicationUser> mUserManager;
        private SignInManager<ApplicationUser> mSignInManager;

        public IConfiguration Configuration { get; }

        public AccountController(AccountsDbContext context, DataDbContext dataDbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            mroleManager = roleManager;
            mdataDbContext = dataDbContext;
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.CurrentView = "login";
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("System", "App");
            }
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
                    checkdtb(signInModel.DatabaseName);
                    return RedirectToAction("System", "App");
                }
            }
            ModelState.AddModelError("", "Wrong Entry");

            return View();

        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.CurrentView = "Register";
            return View();
        }
        public IActionResult z3eem()
        {
            ViewBag.CurrentView = "z3eem";
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexViewModel mod)
        {

            Register registerModel = mod.Register;

            var database = mContext.Admins.Where(dt => dt.DatabaseName == registerModel.DataBaseName);

            if (database != null)
            {


                var result = await mUserManager.CreateAsync(new ApplicationUser
                {
                    Email = registerModel.Email,
                    DatabaseName = registerModel.DataBaseName,
                    Country = registerModel.Country,
                    Language = registerModel.Language,
                    UserName = registerModel.UserName

                }, registerModel.Password);


                if (result.Succeeded)
                {


                    var roleName = "Adminstrator";
                    checkdtb(registerModel.DataBaseName);
                    var role = await mroleManager.RoleExistsAsync(roleName);
                    if (!role)
                    {
                        await mroleManager.CreateAsync(new ApplicationRole(roleName));
                    }
                    var user = await mUserManager.FindByEmailAsync(registerModel.Email);                   
                    await mUserManager.AddToRoleAsync(user, roleName);
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
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await mSignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
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
            }

            return RedirectToAction("System", "App");
        }
        private void checkdtb(string Database)
        {
            mdataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database="
                    + Database + ";Trusted_Connection=True;MultipleActiveResultSets=true";
            mdataDbContext.Database.EnsureCreated();
        }
    }
}