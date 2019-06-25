using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Interfaces;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingApiController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICustomerRepository _iCustomerRepository;
        public AccountingApiController(ICustomerRepository iCustomerRepository, IProductRepository iProductRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _iCustomerRepository = iCustomerRepository;
            _iProductRepository = iProductRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _iCustomerRepository = iCustomerRepository;
        }

        [HttpGet("GetSoldProduct")]
        public async Task<ActionResult<List<ProductSold>>> GetSoldProduct()
        {
            byte[] error = new byte[500];
            List<ProductSold> products = await _iProductRepository.getSoldProduct(error);
          
            string z = Encoding.ASCII.GetString(error);
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

        [HttpGet("GetInvoice")]
        public async Task<ActionResult<List<Invoice>>> GetInvoice()
        {
            byte[] error = new byte[500];
            List<Invoice> invoices = await _iProductRepository.getInvoice(error);

            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {
                return Ok(invoices);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ActionResult<List<Customer>>> getCustomerById(string id)
        {
            try
            {

                byte[] error = new byte[500];
                List<Customer> customer = await _iCustomerRepository.getCustomerById(id, error);
                string z = Encoding.ASCII.GetString(error);
                string y = z.Remove(z.IndexOf('\0'));
                if (y == "")
                {

                    return Ok(customer);
                }
                else
                {
                    return BadRequest(y);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpGet("getCustomerOrders/{id}")]
        public async Task<ActionResult<List<AOrder>>> getCustomerOrders(string id)
        {
            byte[] error = new byte[500];
            List<AOrder> order = await _iCustomerRepository.getCustomerOrders(id, error);
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

        [HttpGet("getOrderProducts/{id}")]
        public async Task<ActionResult<List<AProduct>>> getOrderProducts(string id)
        {
            byte[] error = new byte[500];
            List<AProduct> product = await _iProductRepository.getOrderProducts(id, error);
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

        [HttpGet("GetCustomerAccount/{id}")]
        public async Task<ActionResult<List<Account>>> getCustomerAccount(string id)
        {
            byte[] error = new byte[500];
            List<Account> account = await _iCustomerRepository.getCustomerAccount(id, error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(account);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("reporting")]
        public async Task<ActionResult<List<Out>>> reporting()
        {
            byte[] error = new byte[500];
            List<Out> an_out = await _iCustomerRepository.reporting(error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(an_out);
            }
            else
            {
                return BadRequest(y);
            }
        }

    }
}