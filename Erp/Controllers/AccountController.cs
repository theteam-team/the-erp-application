 using Erp.Data;
using Erp.Data.Entities;
using Erp.Database_Builder;
using Erp.Interfaces;
using Erp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private IAuthenticationSchemeProvider _authenticationProvider;
        private IServiceProvider _service;
        private IOrganizationRepository _organizationRepository;
        private ModulesDatabaseBuilder _databaseBuilder;
        private IConfiguration _config;
        private Management _management;  // This object used to integrate the management system with this Controller.
        private ILogger<ApplicationUser> muserLogger;
        private DataDbContext mdataDbContext;   // This object Used as entry to the database created to store the system data with respect to a specific user.
        private AccountDbContext mContext;   // this object used as an entry to the database creaded to store Accounts information.      
        private UserManager<ApplicationUser> _userManager;  //used to manage the user stored in the database according to an API.
        private SignInManager<ApplicationUser> _signInManager; //used To sign in the user according to an Api.
        public AccountController(IAuthenticationSchemeProvider authenticationProvider, IServiceProvider service, IOrganizationRepository organizationRepository, AccountDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> userlogger, ModulesDatabaseBuilder databaseBuilder ,Management management, IConfiguration config)
        {
            _authenticationProvider = authenticationProvider;
            _service = service;
            _organizationRepository = organizationRepository;
            _databaseBuilder = databaseBuilder;
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
            if (User.IsInRole("Employee"))
            {

                return RedirectToAction("System", "App");
            }
            ViewBag.CurrentView = "login";
      
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IndexViewModel mod)
        {
            LoginModel signInModel = mod.LoginModel;
            var user = await _userManager.FindByNameAsync(signInModel.UserName);
            if (user != null && signInModel.DatabaseName == user.DatabaseName)
            {
                if ((await _management.GetUserRoleAsync(user)).Contains("Employee"))
                {
                    var result = await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,
                        true, false);
                    if (result.Succeeded)
                    {

                        await _userManager.AddClaimAsync(user, new Claim("organization", user.DatabaseName));
                        await _userManager.AddClaimAsync(user, new Claim("organizationId", user.OrganizationId));

                        var roles = await _userManager.GetRolesAsync(user);
                        muserLogger.LogInformation("A user with a specifc roles : " + roles);

                        return RedirectToAction("System", "App");
                    }
                }
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
            Organization organizationExist = await _organizationRepository.OraganizationExist(registerModel.DatabaseName);            
            if (organizationExist == null)
            {
                var user = new ApplicationUser
                {
                    Email = registerModel.Email,
                    DatabaseName = registerModel.DatabaseName,
                    Country = registerModel.Country,
                    Language = registerModel.Language,
                    UserName = registerModel.UserName,
                    Organization = new Organization {
                        Name = registerModel.DatabaseName,
                        Email = registerModel.Email

                    }
                    
                };



               
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    List<Task> tasks = new List<Task>();
                    var roleAdmin = "Administrator";
                    var roleEmployee = "Employee";
                    await _management.AddRoleToUserAsync(roleAdmin, user);
                    await _management.AddRoleToUserAsync(roleEmployee, user);
                    await _userManager.AddClaimAsync(user, new Claim("organization", user.DatabaseName));
                    await _userManager.AddClaimAsync(user, new Claim("organizationId", user.OrganizationId));
                    muserLogger.LogInformation("A user with a specifc roles : " + roleAdmin + " has Been Created");

                    var res = await _signInManager.PasswordSignInAsync(user.UserName, registerModel.Password,
                        true, false);
                    if (res.Succeeded)
                    {
                        
                        tasks.Add(  _databaseBuilder.createModulesDatabaseAsync(registerModel.DatabaseName));
                        tasks.Add(Task.Run(()=>_authenticationProvider.AddScheme(new AuthenticationScheme(registerModel.DatabaseName, registerModel.DatabaseName, typeof(CookieAuthenticationHandler)))));
                       
 ;
                        
                        await Task.WhenAll(tasks);
                        using (var scope = _service.CreateScope())
                        {
                            var _sysServices = scope.ServiceProvider.GetRequiredService<IServiceCollection>();
                            var shema = _sysServices.AddAuthentication()
                              
                             .AddCookie(registerModel.DatabaseName, o =>
                             {
                                 o.ExpireTimeSpan = TimeSpan.FromHours(1);
                                 o.LoginPath = new PathString("/store/{OrganizationName}");
                                 o.Cookie.Name = registerModel.DatabaseName + " CustomerCookie";
                                 o.SlidingExpiration = true;
                             });

                        }
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
                await _userManager.AddClaimAsync(user, new Claim("database", user.DatabaseName));

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
                          expires: DateTime.Now.AddMinutes(1000),
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