using Erp.Interfaces;
using Erp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseApiController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IOrderRepository _orderRepository;

        public WarehouseApiController(IProductRepository iProductRepository, IOrderRepository orderRepository)
        {
            _iProductRepository = iProductRepository;
            _orderRepository = orderRepository;
        }
        [HttpPost("AddProduct")]
        public async Task<ActionResult<string>> AddProduct(Product product)
        {
            byte[] error = new byte[500];
            int status = await _iProductRepository.Create(product, error);
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
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<string>> DeleteProduct(Product product)
        {
            byte[] error = new byte[500];
            int status = await _iProductRepository.Delete(product, error);
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
        [HttpGet("CheckUnitsInStock/{id}")]
        public async Task<ActionResult<int>> CheckUnitsInStock(string id)
        {
            byte[] error = new byte[500];
            int units = await _iProductRepository.checkunitsInStock(id, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok("Units = " + units);
            }
            else
            {
                return BadRequest(y);
            }
        }
        [HttpPut("AddToStock/{id}")]
        public async Task<ActionResult<int>> AddToStock(string id, int newUnits)
        {
            byte[] error = new byte[500];
            int status = await _iProductRepository.addToStock(id, newUnits, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (status == 0)
            {

                return Ok("Succesfully Added");
            }
            else
            {
                return BadRequest(y);
            }
        }
        [HttpDelete("RemoveFromStock/{id}")]
        public async Task<ActionResult<int>> RemoveFromStock(string id, int newUnits)
        {
            byte[] error = new byte[500];
            int status = await _iProductRepository.removeFromStock(id, newUnits, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (status == 0)
            {

                return Ok("Succesfully removed");
            }
            else
            {
                return BadRequest(y);
            }
        }
        [HttpPut("UpdateProductInfo/{id}")]
        public async Task<ActionResult<int>> UpdateProductInfo(string id, string key, string value)
        {
            //Console.WriteLine(id);
            byte[] error = new byte[500];
            int status = await _iProductRepository.UpdateInfo(id, key, value, error);
            string z = System.Text.Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (status == 0)
            {

                return Ok("Succesfully Modified");
            }
            else
            {
                return BadRequest(y);
            }
        }


        [HttpDelete("DeleteProductById/{id}")]
        public async Task<ActionResult<string>> DeleteProductById(string id)
        {

            byte[] error = new byte[500];
            int status = await _iProductRepository.Delete(id, error);
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

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            byte[] error = new byte[500];
            Product product = await _iProductRepository.GetById(id, error);
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
        [HttpGet("GetProductInfo/{id}")]
        public async Task<ActionResult<Product>> GetProductInfo(string id, string key)
        {
            byte[] error = new byte[500];
            string info = await _iProductRepository.getInfo(id, key, error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(info);
            }
            else
            {
                return BadRequest(y);
            }
        }
        [HttpGet("ShowProducts")]
        public async Task<ActionResult<List<Product>>> ShowProducts()
        {
            byte[] error = new byte[500];
            List<Product> products = await _iProductRepository.GetAll(error);
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
        [HttpGet("GetOrderInfo/{id}")]
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
        [HttpGet("ShowCompletedOrders")]
        public async Task<ActionResult<List<Order>>> ShowCompletedOrders()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowCompletedOrders(error);
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
        [HttpGet("ShowOrdersInProgress")]
        public async Task<ActionResult<List<Order>>> ShowOrdersInProgress()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowOrdersInProgress(error);
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
        [HttpGet("ShowProductsInOrder/{id}")]
        public async Task<ActionResult<List<Product_In_Order>>> ShowProductsInOrder(string id)
        {
            byte[] error = new byte[500];
            List<Product_In_Order> orders = await _orderRepository.ShowProductsInOrder(id, error);
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
    }
}
