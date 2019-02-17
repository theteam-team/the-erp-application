using ErpApplication.Data;
using ErpApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ErpApplication.Controllers
{
    public class AccountController : Controller
    {
        private DataDbContext mdataDbContext;
        protected AccountsDbContext mContext;
        private UserManager<AdminsTable> mUserManager;
        private SignInManager<AdminsTable> mSignInManager;

        public IConfiguration Configuration { get; }

        public AccountController(AccountsDbContext context, DataDbContext dataDbContext,IConfiguration configuration ,
            UserManager<AdminsTable> userManager,
            SignInManager<AdminsTable> signInManager)
        {
            mdataDbContext = dataDbContext;
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.CurrentView = "login";
            //mContext.Database.EnsureCreated();
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

            var user = mContext.Admins.Where(dt => dt.DatabaseName == signInModel.DatabaseName).First();

            mdataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=" + signInModel.DatabaseName + ";Trusted_Connection=True;MultipleActiveResultSets=true";

            mdataDbContext.Database.EnsureCreated();

            if (signInModel.UserName == user.UserName)
            {
                var result = await mSignInManager.PasswordSignInAsync(user.UserName, signInModel.Password,
                        true, false);
                if (result.Succeeded)
                    return RedirectToAction("System", "App");
            }
            ModelState.AddModelError("","Wrong Password Or UserName");

            return View();
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.CurrentView = "register";
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexViewModel mod)
        {
           
            Register registerModel = mod.Register;
          
            
            //mContext.Database.EnsureCreated();
            var result = await mUserManager.CreateAsync(new AdminsTable
            {
                Email = registerModel.Email,
                DatabaseName = registerModel.DataBasaName,
                Country = registerModel.Country,
                Language = registerModel.Language,
                UserName = registerModel.UserName

            }, registerModel.Password);

           
            if (result.Succeeded)
            {
                mdataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database=" + registerModel.DataBasaName + ";Trusted_Connection=True;MultipleActiveResultSets=true";
                mdataDbContext.Database.EnsureCreated();

                var user = await mUserManager.FindByEmailAsync(registerModel.Email);
                var res = await mSignInManager.PasswordSignInAsync(user.UserName, registerModel.Password,
                    true, false);
                if (res.Succeeded)
                    return RedirectToAction("System", "App");
                else
                    return View();
            }
            else
                return View(); 
          
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await mSignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}