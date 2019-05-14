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
        private readonly IOrderProductRepository _orderProductRepository;

        public WarehouseApiController(IProductRepository iProductRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _iProductRepository = iProductRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
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

        [HttpPost("AddOrder")]
        public async Task<ActionResult<string>> AddOrder(Order order)
        {
            byte[] error = new byte[500];
            int status = await _orderRepository.Create(order, error);
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

        [HttpPost("AddProductToOrder")]
        public async Task<ActionResult<string>> AddProductToOrder(ProductInOrder product)
        {
            byte[] error = new byte[500];
            int status = await _orderProductRepository.Create(product, error);
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

        [HttpPut("EditProduct")]
        public async Task<ActionResult<string>> EditProduct(Product product)
        {
            byte[] error = new byte[500];
            int status = await _iProductRepository.EditProduct(product, error);
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

        [HttpPut("EditOrder")]
        public async Task<ActionResult<string>> EditOrder(Order order)
        {
            byte[] error = new byte[500];
            int status = await _orderRepository.EditOrder(order, error);
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

        [HttpPut("EditProductInOrder")]
        public async Task<ActionResult<string>> EditProductInOrder(ProductInOrder product)
        {
            byte[] error = new byte[500];
            int status = await _orderProductRepository.EditProductInOrder(product, error);
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

        [HttpPut("RemoveFromStock")]
        public async Task<ActionResult<int>> RemoveFromStock(ProductInOrder product)
        {
            byte[] error = new byte[500];
            int status = await _orderProductRepository.removeFromStock(product, error);
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

        [HttpDelete("DeleteOrderById/{id}")]
        public async Task<ActionResult<string>> DeleteOrderById(string id)
        {

            byte[] error = new byte[500];
            int status = await _orderRepository.Delete(id, error);
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

        [HttpDelete("DeleteProductFromOrder/{oid}/{pid}")]
        public async Task<ActionResult<string>> DeleteProductFromOrder(string oID, string pID)
        {

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

        [HttpGet("ShowAllOrders")]
        public async Task<ActionResult<List<Order>>> ShowAllOrders()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.GetAll(error);
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

        [HttpGet("ShowReadyOrders")]
        public async Task<ActionResult<List<Order>>> ShowReadyOrders()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowReadyOrders(error);
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
        public async Task<ActionResult<List<ProductInOrder>>> ShowProductsInOrder(string id)
        {
            byte[] error = new byte[500];
            List<ProductInOrder> orders = await _orderProductRepository.ShowProductsInOrder(id, error);
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

        [HttpGet("SearchProducts/{key}/{value}")]
        public async Task<ActionResult<List<Product>>> SearchProducts(string key, string value)
        {
            byte[] error = new byte[500];
            List<Product> products = await _iProductRepository.SearchProducts(key, value, error);
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

        [HttpGet("SearchOrders/{key}/{value}")]
        public async Task<ActionResult<List<Product>>> SearchOrders(string key, string value)
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.SearchOrders(key, value, error);
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
