using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Erp.Data;
using Erp.ModulesWrappers;
using Erp.ViewModels.CRN_Tabels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Erp.Controllers
{
    /// <summary>
    /// Web Api To Contact The CRM modules Web Services
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CRM_APIController : ControllerBase
    {
        private DataDbContext _dataDbContext;  //object to connect with the Database Containing The Crm Data
        /// <summary>
        /// a constructror gets its services parameters resolved by the runtime from the service container
        /// </summary>
        /// <param name="dataDbContext"></param>
        public CRM_APIController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;

        }
        /// <summary>
        /// rest web service that responds to a post http request with a body containing an object of the Class numbers
        /// This web service Contact with a specific C++ Function in the CRM dll through a wrapper to serve the request
        /// </summary>
        /// <param name="numbers">The object passed To the web service </param>
        /// <returns>the summation of the fields of the passed object </returns>
        //[HttpPost("AddNumber")]
        //public ActionResult<int> AddNumbers(Numbers numbers)
        //{
        //    return Crm_Wrapper.AddNumbers(numbers.N1, numbers.N2) ;
        //}

        /// <summary>
        /// rest web service that responds to a post http request with a body containing an object of the Class numbers
        /// This web service Contact with a specific C++ Function in the CRM dll through a wrapper to serve the request
        /// </summary>
        /// <param name="numbers">The object passed To the web service </param>
        /// <returns>the multiplication of the fields of the passed object </returns>
        //[HttpPost("MultiplyNumbers")]
        //public ActionResult<int> MultiplyNumbers(Numbers numbers)
        //{
        //    return Crm_Wrapper.MultiplyNumbers(numbers.N1, numbers.N1);
        //}

        [HttpPost("AddCustomer")]
        public ActionResult<string> AddCustomer(Customer customer)
        {
            byte[] error = new byte[100];
            int status = Crm_Wrapper.AddCustomer(customer, error);
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
        public ActionResult<string> AddOpportunities(Opportunities_product opportunities_Product)
        {
            byte[] error = new byte[100];
            int status = Crm_Wrapper.AddOpportunity(opportunities_Product.Opportunities, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            Console.WriteLine("status = "+ status);
            if (status == 0)
            {
                for (int i = 0; i < opportunities_Product.product_id.Count; ++i)
                {
                    byte[] _error = new byte[100];
                    status = Crm_Wrapper.AddOpportunitie_detail(opportunities_Product.Opportunities.opportunity_id,
                        opportunities_Product.product_id[i], _error);
                    z = System.Text.Encoding.ASCII.GetString(_error);
                    z.Remove(z.IndexOf('\0'));
                    if (status != 0)
                    {

                        return BadRequest(z.Remove(z.IndexOf('\0')));
                    }
                }
            }
            else
            {
                return BadRequest(z.Remove(z.IndexOf('\0')));
            }
             return Ok("successfuly added");
            
        }
        [HttpGet("GetCustomer/{id}")]
        public ActionResult<Customer> GetCustomer(string id)
        {
            IntPtr outArray;
            IntPtr statusPtr;
            byte[] error = new byte[100];      
            Crm_Wrapper.getCustomerById(id, out outArray, out statusPtr, error);
            int status = Marshal.ReadInt32(statusPtr);
            Marshal.FreeCoTaskMem(statusPtr);
            string z = System.Text.Encoding.ASCII.GetString(error);
            if (status == 0)
            {
                Customer customer = (Customer)Marshal.PtrToStructure(outArray, typeof(Customer));
                Marshal.FreeCoTaskMem(outArray);
                return Ok(customer);
            }
            else
                return BadRequest(z.Remove(z.IndexOf('\0')));
        }



    }
}
