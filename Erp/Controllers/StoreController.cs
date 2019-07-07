using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Erp.Models;
using Erp.ViewModels;
using Microsoft.AspNetCore.Authentication;
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
        private TaskExectionQueue _executionEngine;
        private IAuthenticationService _authorizationService;
        //private static string AuthSchemes = null;
        
        public IProductRepository _productRepository { get; }
        public ICustomerRepository _customerRepository { get; }
        public IOrganizationRepository _organizationRepository { get; }
        public IOrderRepository _orderRepository{ get; }
        public IOrderProductRepository _orderProductRepository { get; }
        public ICustomerProductRepository _customerProductRepository { get; }
        public IAddressRepository _addressRepository { get; }
        public IPaymentRepository _paymentRepository { get; }
        private IConfiguration _config;
        private Management _management; 
        private ILogger<ApplicationUser> muserLogger;
       
        private AccountDbContext mContext;  
        private UserManager<ApplicationUser> _userManager;  
        private SignInManager<ApplicationUser> _signInManager; 
        public StoreController(TaskExectionQueue executionEngine ,IAuthenticationService authorizationService, IProductRepository productRepository,ICustomerRepository customerRepository ,IOrganizationRepository organizationRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, ICustomerProductRepository customerProductRepository, IAddressRepository addressRepository, IPaymentRepository paymentRepository, AccountDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> userlogger, Management management, IConfiguration config)
        {
            _executionEngine = executionEngine;
            _authorizationService = authorizationService;
            //AuthSchemes = (string)RouteData.Values["OrganizationName"];
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _organizationRepository = organizationRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _customerProductRepository = customerProductRepository;
            _addressRepository = addressRepository;
            _paymentRepository = paymentRepository;
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
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);

            if (orgExist != null)
            {
                var result = await  _authorizationService.AuthenticateAsync(HttpContext, OrganizationName);
                if (result.Succeeded)
                {
                    ViewBag.Organization = OrganizationName;
                    HttpContext.Session.SetString(HttpContext.Session.Id, OrganizationName);
                    return View("ProductManager");
                }
                else
                {
                    return LocalRedirect("~/Store/" + orgExist.Name+"/Login");
                }
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
                if (user != null && orgExist.Name == user.DatabaseName)
                {
                    string hashProvider = _userManager.PasswordHasher.HashPassword(user, loginModel.Password);
                    var result =  _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, signInModel.Password);
                    if (result == PasswordVerificationResult.Success)
                    Console.WriteLine("hereee");
                    {
                        if ((await _management.GetUserRoleAsync(user)).Contains("Customer"))
                        {
                            var roleCustomer = "Customer";
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id),
                                new Claim(ClaimTypes.Role, roleCustomer),
                                new Claim("organization", orgExist.Name),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, OrganizationName);
                            var authProperties = new AuthenticationProperties();


                            await Task.WhenAll(
                                new Task[]{
                                HttpContext.SignInAsync(OrganizationName, new ClaimsPrincipal(claimsIdentity), authProperties)
                                });

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
                    var claims = new List<Claim>
                    {
                      new Claim(ClaimTypes.Name, user.UserName),
                      new Claim(ClaimTypes.NameIdentifier, user.Id),
                      new Claim(ClaimTypes.Role, roleCustomer),
                      new Claim("organization", orgExist.Name),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, OrganizationName);
                    var authProperties = new AuthenticationProperties();
                    await Task.WhenAll(
                        new Task[]{
                        _management.AddRoleToUserAsync(roleCustomer, user),
                    HttpContext.SignInAsync(OrganizationName, new ClaimsPrincipal(claimsIdentity), authProperties)
                    });
                    var Customer = new Customer()
                    {
                        customer_id = user.Id,
                        name = customerRegister.name,
                        phone_number = customerRegister.phoneNumber,
                        email = customerRegister.Email,
                        DateOfBirth = customerRegister.DateOfBirth,
                        loyality_points = 0,
                        type = 0,
                        is_lead = false,
                    };

                    byte[] error = new byte[500];
                    _customerRepository.setConnectionString(OrganizationName);
                    int status = await _customerRepository.Create(Customer, error);

                    if (status != 0)
                    {
                        return StatusCode(500);
                    }

                    muserLogger.LogInformation("A user with a specifc roles : " + roleCustomer + " has Been Created");
                    return LocalRedirect("~/Store/"+orgExist.Name);
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
           string OrganizationName = (string)RouteData.Values["OrganizationName"];
           await HttpContext.SignOutAsync(OrganizationName);
           Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);

            return LocalRedirect("~/Store/" + orgExist.Name + "/Login");
        }

        [HttpGet("GetProductStore")]
        public async Task<ActionResult<List<Product>>> GetProductStore()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _productRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                List<Product> product = await _productRepository.ShowAvailableProducts(error);
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

        [HttpGet("GetUserId")]
        public async Task<ActionResult<string>> GetUserId()
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];

            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                var result = await _authorizationService.AuthenticateAsync(HttpContext, OrganizationName);
                if (result.Succeeded)
                {
                    return ((ClaimsIdentity)result.Principal.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPost("AddPotentialOrder")]
        public async Task<ActionResult<string>> AddOrderStore([FromBody]Order order)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderRepository.AddPotentialOrder(order, error);
                string z = System.Text.Encoding.ASCII.GetString(error);
                if (status != 0)
                {
                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPost("AddToOrder")]
        public async Task<ActionResult<string>> AddToOrderStore([FromBody]ProductInOrder product)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderProductRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderProductRepository.AddPotentialProduct(product, error);
                string z = System.Text.Encoding.ASCII.GetString(error);
                if (status != 0)
                {
                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPost("AddCustomerAddress")]
        public async Task<ActionResult<string>> AddOCustomerAddress([FromBody]Address address)
        {
            Console.WriteLine("hererer");
            
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                var result = await _authorizationService.AuthenticateAsync(HttpContext, OrganizationName);
                if (result.Succeeded)
                {
                    address.customer_id = ((ClaimsIdentity)result.Principal.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                    Console.WriteLine(address.customer_id);
                    _addressRepository.setConnectionString(OrganizationName);

                    byte[] error = new byte[500];
                    int status = await _addressRepository.AddCustomerAddress(address, error);
                    string z = System.Text.Encoding.ASCII.GetString(error);
                    if (status != 0)
                    {
                        return BadRequest(z.Remove(z.IndexOf('\0')));
                    }
                    else
                    {
                        return Ok("successfuly added");
                    }
                }
                else
                    return Unauthorized();
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPost("AddPayment")]
        public async Task<ActionResult<string>> AddPayment([FromBody]Payment payment)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _paymentRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _paymentRepository.AddPayment(payment, error);
                string z = Encoding.ASCII.GetString(error);
                if (status != 0)
                {

                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPut("AddOrderPayment")]
        public async Task<ActionResult<string>> AddOrderPayment([FromBody]Order order)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderRepository.AddOrderPayment(order, error);
                string z = Encoding.ASCII.GetString(error);
                if (status != 0)
                {
                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    BpmTask task = new BpmTask()
                    {
                        IsBpm = false,
                        type = "external",
                        TaskName = "InvokeOrder",
                        databaseName = OrganizationName,
                        TaskParam = new object[] { order }
                    };
                    _executionEngine.QueueExection(task);
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpGet("GetCustomerProducts/{id}")]
        public async Task<ActionResult<List<CustomerProduct>>> ShowCustomerProducts(string id)
        {
            Console.WriteLine("customer = " +id);
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _customerProductRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                List<CustomerProduct> orders = await _customerProductRepository.ShowCustomerProducts(id, error);
                Console.WriteLine("order" + orders.Count);
                string z = Encoding.ASCII.GetString(error);
                string y = z.Remove(z.IndexOf('\0'));
                if (y == "")
                {

                    return Ok(orders);
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

        [HttpDelete("DeleteCustomerProduct/{oid}/{pid}")]
        public async Task<ActionResult<string>> DeleteCustomerProduct(string oID, string pID)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderProductRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderProductRepository.DeleteProductFromOrder(oID, pID, error);
                string z = System.Text.Encoding.ASCII.GetString(error);
                if (status != 0)
                {

                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly deleted");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPut("AddToOrderTotal")]
        public async Task<ActionResult<string>> AddToOrderTotal([FromBody]Order order)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderRepository.AddToOrderTotal(order, error);
                string z = System.Text.Encoding.ASCII.GetString(error);
                if (status != 0)
                {

                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpPut("RemoveFromOrderTotal")]
        public async Task<ActionResult<string>> RemoveFromOrderTotal([FromBody]Order order)
        {
            string OrganizationName = (string)RouteData.Values["OrganizationName"];
            Organization orgExist = await _organizationRepository.OraganizationExist(OrganizationName);
            if (orgExist != null)
            {
                _orderRepository.setConnectionString(OrganizationName);

                byte[] error = new byte[500];
                int status = await _orderRepository.RemoveFromOrderTotal(order, error);
                string z = System.Text.Encoding.ASCII.GetString(error);
                if (status != 0)
                {

                    return BadRequest(z.Remove(z.IndexOf('\0')));
                }
                else
                {
                    return Ok("successfuly added");
                }
            }
            else
            {
                return NotFound("This organization does not exist");
            }
        }

        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult<Order>> GetOrderInfo(string id)
        {
            byte[] error = new byte[500];
            Order order = await _orderRepository.GetById(id, error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(order);
            }
            else
            {
                return BadRequest(y);
            }
        }
    }
}
