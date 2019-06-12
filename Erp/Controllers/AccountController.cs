 using Erp.Data;
using Erp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Controllers
{  /// <summary>
   /// The Countroller That handels User login, logout and Registeration
   /// </summary>
    public class AccountController : Controller
    {
        private int x = 10;
        private IConfiguration _config;
        private Management _management;  // This object used to integrate the management system with this Controller.
        private ILogger<ApplicationUser> muserLogger;
        private DataDbContext mdataDbContext;   // This object Used as entry to the database created to store the system data with respect to a specific user.
        private AccountDbContext mContext;   // this object used as an entry to the database creaded to store Accounts information.      
        private UserManager<ApplicationUser> _userManager;  //used to manage the user stored in the database according to an API.
        private SignInManager<ApplicationUser> _signInManager; //used To sign in the user according to an Api.
        public AccountController(AccountDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> userlogger, Management management, IConfiguration config)
        {
            _config = config;
            _management = management;
            muserLogger = userlogger;
            mdataDbContext = dataDbContext;
            mContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {

                if (HttpContext.Session.GetString("LastPageView") != null)
                    return Redirect(HttpContext.Session.GetString("LastPageView"));
                return RedirectToAction("System", "App");
            }
            ViewBag.CurrentView = "login";
            //HttpContext.Session.SetString("LastPageView", HttpContext.Request.Path);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IndexViewModel mod)
        {

            LoginModel signInModel = mod.LoginModel;

            var user = await _userManager.FindByNameAsync(signInModel.UserName);

            if (user != null && signInModel.DatabaseName == user.DatabaseName)
            {
                if (CommonNeeds.checkdtb(mdataDbContext, signInModel.DatabaseName))
                {
                    var result = await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,
                            true, false);
                    if (result.Succeeded)
                    {
                      

                        var roles = await _userManager.GetRolesAsync(user);

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
            
            ViewBag.CurrentView = "register";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexViewModel mod)
        {
            Register registerModel = mod.Register;

            var database = mContext.ErpUsers.Where(dt => dt.DatabaseName == registerModel.DataBaseName);

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
                var result = await _userManager.CreateAsync(user, registerModel.Password);


                if (result.Succeeded)
                {

                    var roleName = "Adminstrator";

                    await _management.AddRoleToUserAsync(roleName, user);

                    muserLogger.LogInformation("A user with a specifc roles : " + roleName + " has Been Created");

                   

                    if (!CommonNeeds.checkdtb(mdataDbContext, registerModel.DataBaseName))
                    {
                        mdataDbContext.Database.EnsureCreated();
                    }
                    var res = await _signInManager.PasswordSignInAsync(user.UserName, registerModel.Password,
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

            var user = await _userManager.GetUserAsync(User);

            var client = (new ApplicationUser
            {
                Email = registerModel.Email,
                DatabaseName = user.DatabaseName,
                Country = registerModel.Country,
                Language = registerModel.Language,
                UserName = registerModel.UserName

            });
            var result = await _userManager.CreateAsync(client, registerModel.Password);
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
            if (HttpContext.Session.GetString("LastPageView") != null)
                return Redirect(HttpContext.Session.GetString("LastPageView"));

            return RedirectToAction("App", "System");

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            muserLogger.LogInformation("A user With specific Roles : ");
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var el in roles)
            {
                Console.Write(" " + el);
            }
            Console.Write("Has been Logged out of the System");
            return RedirectToAction("Login", "Account");
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
       

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        // Create the token
                        var claims = new List<Claim>()
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        };

                        foreach (var role in await _management.GetUserRoleAsync(user))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          _config["Tokens:Issuer"],
                          _config["Tokens:Audience"],
                          claims,
                          expires: DateTime.Now.AddMinutes(100000000),
                          signingCredentials: creds);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }
           return BadRequest();

        }
    }
}