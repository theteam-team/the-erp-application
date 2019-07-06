using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Erp.Data;
using Erp.ModulesWrappers;
using Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Erp.ViewModels.CRN_Tabels;
using Erp.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Erp.Controllers
{
    /// <summary>
    /// Web Api To Contact The CRM modules Web Services
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CrmApiController : ControllerBase
    {
        public ICustomerRepository _customerRepository { get; set; }
        public IOpportunityRepository _opportunityRepository { get; set; }
        public IEmployeeRepository _employeeRepository { get; set; }
        public IOpportunityProductRepository _opportunityProductRepository { get; set; }

        public CrmApiController (ICustomerRepository customerepository , IOpportunityRepository opportunityRepository, IEmployeeRepository employeeRepository, IOpportunityProductRepository opportunityProductRepository)
        {
            _employeeRepository = employeeRepository;
            _customerRepository = customerepository;
            _opportunityRepository = opportunityRepository;
            _opportunityProductRepository = opportunityProductRepository;
        }  
        

        [HttpPost("AddCustomer")]
        public  async Task<ActionResult<string>> AddCustomer(Customer customer)
        {
            Console.WriteLine("customer  "+customer.customer_id);
            byte[] error = new byte[100];
            int status = await _customerRepository.Create(customer, error);
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

        [HttpPost("AddOpportunity")]
        public async Task<ActionResult<string>> AddOpportunity(Opportunity opportunity)
        {
            byte[] error = new byte[500];
            int status = await _opportunityRepository.Create(opportunity, error);
            
            if (status != 0)
            {
                string z = System.Text.Encoding.ASCII.GetString(error);
                z.Remove(z.IndexOf('\0'));
                return BadRequest(z.Remove(z.IndexOf('\0')));                
            }
             return Ok("successfuly added");           
        }

        [HttpPost("AddOpportunityProduct")]
        public async Task<ActionResult<string>> AddOpportunityProduct(OpportunityProduct product)
        {
            byte[] error = new byte[500];
            int status = await _opportunityProductRepository.Create(product, error);

            if (status != 0)
            {
                string z = System.Text.Encoding.ASCII.GetString(error);
                z.Remove(z.IndexOf('\0'));
                return BadRequest(z.Remove(z.IndexOf('\0')));
            }
            return Ok("successfuly added");
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            Console.WriteLine(id);
            byte[] err = new byte[100];
            Customer customer =  await _customerRepository.GetById(id, err);
            string z = System.Text.Encoding.ASCII.GetString(err);
            string error = z.Remove(z.IndexOf('\0'));
            if (customer != null)
            {               
                return Ok(customer);
            }
            else
                return BadRequest(error);
        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            byte[] error = new byte[500];
            List<Customer> customers = await _customerRepository.GetAll(error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {
                return Ok(customers);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("GetAllOpportunities")]
        public async Task<ActionResult<List<Opportunity>>> GetAllOpportunities()
        {
            byte[] error = new byte[500];
            List<Opportunity> opportunities = await _opportunityRepository.GetAll(error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {
                return Ok(opportunities);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("GetAllOpportunityProduct/{id}")]
        public async Task<ActionResult<List<OpportunityProduct>>> GetAllOpportunityProduct(string id)
        {
            byte[] error = new byte[500];
            List<OpportunityProduct> products = await _opportunityProductRepository.GetAllOpportunityProducts(id, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {
                return Ok(products);
            }
            else
            {
                return BadRequest(y);
            }
        }
    }
}
