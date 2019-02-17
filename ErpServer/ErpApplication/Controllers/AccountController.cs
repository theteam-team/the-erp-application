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
        protected AccountsDbContext mContext;
        private UserManager<AdminsTable> mUserManager;
        private SignInManager<AdminsTable> mSignInManager;

        public IConfiguration Configuration { get; }

        public AccountController(AccountsDbContext context, IConfiguration configuration ,
            UserManager<AdminsTable> userManager,
            SignInManager<AdminsTable> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Login()
        {
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
            SignInModel signInModel = mod.SignInModel;

            var user = await mUserManager.FindByEmailAsync(signInModel.Email);
            var result = await mSignInManager.PasswordSignInAsync(user.UserName, signInModel.Password,
                    true, false);
            if (result.Succeeded)
                return RedirectToAction("System", "App");

            ModelState.AddModelError("","Wrong Password Or Email");

            return View();
            
        }
        [HttpGet]
        public IActionResult Register()
        {
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