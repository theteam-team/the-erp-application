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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Erp.Controllers
{
    /// <summary>
    /// Web Api To Contact The CRM modules Web Services
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CrmApiController : ControllerBase
    {
        public ICustomerRepository _customeRepository { get; set; }
        public IOpportunityRepository _opportunityRepository { get; set; }
        public IEmployeeRepository _employeeRepository { get; set; }

        public CrmApiController ( ICustomerRepository customerepository , IOpportunityRepository opportunityRepository, 
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _customeRepository = customerepository;
            _opportunityRepository = opportunityRepository;

        }
        
        

        [HttpPost("AddCustomer")]
        public  async Task<ActionResult<string>> AddCustomer(Customer customer)
        {
            Console.WriteLine("customer  "+customer.customer_id);
            byte[] error = new byte[100];
            int status = await _customeRepository.Create(customer, error);
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
        [HttpPost("AddOpportunities")]
        public async Task<ActionResult<string>> AddOpportunities(Opportunities_product opportunities_Product)
        {
            byte[] error = new byte[500];
            int status = await _opportunityRepository.Create(opportunities_Product, error);
            
            if (status != 0)
            {
                string z = System.Text.Encoding.ASCII.GetString(error);
                z.Remove(z.IndexOf('\0'));
                return BadRequest(z.Remove(z.IndexOf('\0')));
                
            }
             return Ok("successfuly added");
            
        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<string>> AddEmployee(Employee employee)
        {
            byte[] error = new byte[100];
            int status = await _employeeRepository.Create(employee, error);

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
            Customer customer =  await _customeRepository.GetById(id, err);
            string z = System.Text.Encoding.ASCII.GetString(err);
            string error = z.Remove(z.IndexOf('\0'));
            if (customer != null)
            {
                
                return Ok(customer);
            }
            else
                return BadRequest(error);
        }



    }
}
