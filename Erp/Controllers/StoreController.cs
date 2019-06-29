using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Erp.Models;
using Erp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Erp.Controllers
{
    [Route("[controller]/{OrganizationName}")]
    public class StoreController : Controller
    {
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private IOrganizationRepository _organizationRepository;
       
        private IConfiguration _config;
        private Management _management; 
        private ILogger<ApplicationUser> muserLogger;
       
        private AccountDbContext mContext;  
        private UserManager<ApplicationUser> _userManager;  
        private SignInManager<ApplicationUser> _signInManager; 
        public StoreController(IProductRepository productRepository,ICustomerRepository customerRepository ,IOrganizationRepository organizationRepository, AccountDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> userlogger, Management management, IConfiguration config)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _organizationRepository = organizationRepository;         
            _config = config;
            _management = management;
            muserLogger = userlogger;
            mContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<ActionResult> Store()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Console.WriteLine(OrganizationName);
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {              
                ViewBag.Organization = OrganizationName;               
                HttpContext.Session. SetString (HttpContext.Session.Id, OrganizationName);
                return View("ProductManager");

            }
            else
                return NotFound("This organization does not exist");

        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];           
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            ViewBag.Organization = OrganizationName;
            if (orgExist != null)
            {
                LoginModel signInModel = loginModel;
                var user = await _userManager.FindByNameAsync(signInModel.UserName);
                if (user != null && signInModel.DatabaseName == user.DatabaseName)
                {
                    if ((await _management.GetUserRoleAsync(user)).Contains("Customer"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password,
                                loginModel.RememberMe, false);
                        if (result.Succeeded)
                        {

                            await _userManager.AddClaimAsync(user, new Claim("organization", user.DatabaseName));
                            await _userManager.AddClaimAsync(user, new Claim("organizationId", user.OrganizationId));

                            var roles = await _userManager.GetRolesAsync(user);
                            muserLogger.LogInformation("A user with a specifc roles : " + roles);

                            return LocalRedirect("~/Store/" + orgExist.Name);
                        }
                    }

                }
                else
                    ModelState.AddModelError("", "Wrong Entry");
                return View("CustomerLogin");
            }
            else
                return NotFound("This organization does not exist");

        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Console.WriteLine(OrganizationName);
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            ViewBag.Organization = OrganizationName;
            if (orgExist != null)
            {
                return View("CustomerLogin");
            }else
                return NotFound("This organization does not exist");
        }
        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            ViewBag.Organization = OrganizationName;
            if (orgExist != null)
            {
                return View("CustomerRegister");
            }
            else
                return NotFound("This organization does not exist");
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(CustomerRegister customerRegister)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            ViewBag.Organization = OrganizationName;
           
            if (orgExist!= null)
            {
                var user = new ApplicationUser
                {
                    Email = customerRegister.Email,
                    DatabaseName = OrganizationName,
                    UserName = customerRegister.UserName,
                    OrganizationId = orgExist.Id,
                  
                };

               
                var result = await _userManager.CreateAsync(user, customerRegister.Password);
                if (result.Succeeded)
                {
                    var roleCustomer = "Customer";
              
                    await _management.AddRoleToUserAsync(roleCustomer, user);
                   
                    await _userManager.AddClaimAsync(user, new Claim("organization", user.DatabaseName));
                    await _userManager.AddClaimAsync(user, new Claim("organizationId", user.OrganizationId));
                   
                    var res = await _signInManager.PasswordSignInAsync(user.UserName, customerRegister.Password,
                        true, false);

                    muserLogger.LogInformation("A user with a specifc roles : " + roleCustomer + " has Been Created");
                    if (res.Succeeded)
                    {
                        return LocalRedirect("~/Store/"+orgExist.Name);
                    }

                }
                var errors = result.Errors.ToList();
                foreach (var el in errors)
                {
                    ModelState.AddModelError("", el.Code);
                }
            }
            else
                return NotFound("This organization does not exist");

            return View("CustomerRegister");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);

            return LocalRedirect("~/Store/" + orgExist.Name);
        }
        [HttpGet("GetProductStore")]
        [Authorize(Roles ="Customer")]
        public async Task<ActionResult<List<Product>>> GetProductStore()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _productRepository.setConnectionString(OrganizationName);
                Console.WriteLine(OrganizationName);

                byte[] error = new byte[500];
                List<Product> product = await _productRepository.GetAll(error);
                string z = Encoding.ASCII.GetString(error);
                string y = z.Remove(z.IndexOf('\0'));
                if (y == "")
                {

                    return Ok(product);
                }
                else
                {
                    return BadRequest(y);
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

    }
}
