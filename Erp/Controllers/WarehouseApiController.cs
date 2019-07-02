using Erp.Data;
using Erp.Interfaces;
using Erp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class WarehouseApiController : ControllerBase
    {
        public  IProductRepository _iProductRepository { get; }
        public IOrderRepository _orderRepository { get; }
        public IOrderProductRepository _orderProductRepository { get; }
        public  IInventoryRepository _inventoryRepository { get; }
        public IInventoryProductRepository _inventoryProductRepository { get; }
        public IReportRepository _reportRepository { get; }
        public IProductMovesRepository _productMovesRepository { get; }

        public WarehouseApiController(IProductRepository iProductRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IInventoryRepository inventoryRepository, IInventoryProductRepository inventoryProductRepository, IReportRepository reportRepository, IProductMovesRepository iProductMovesRepository)
        {

            _iProductRepository = iProductRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _inventoryRepository = inventoryRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _reportRepository = reportRepository;
            _productMovesRepository = iProductMovesRepository;
        }

        [HttpPost("AddInventory")]
        public async Task<ActionResult<string>> AddInventory(Inventory inventory)
        {
            byte[] error = new byte[500];
            int status = await _inventoryRepository.Create(inventory, error);
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

        [HttpPost("AddPotentialProduct")]
        public async Task<ActionResult<string>> AddPotentialProduct(ProductInOrder product)
        {
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

        [HttpPost("AddProductToInventory")]
        public async Task<ActionResult<string>> AddProductToInventory(ProductInInventory product)
        {
            byte[] error = new byte[500];
            int status = await _inventoryProductRepository.Create(product, error);
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

        [HttpPut("EditProductInInventory")]
        public async Task<ActionResult<string>> EditProductInInventory(ProductInInventory product)
        {
            byte[] error = new byte[500];
            int status = await _inventoryProductRepository.EditProductInInventory(product, error);
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

        [HttpDelete("DeleteInventory/{id}")]
        public async Task<ActionResult<string>> DeleteInventory(string id)
        {

            byte[] error = new byte[500];
            int status = await _inventoryRepository.Delete(id, error);
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

        [HttpDelete("DeleteProductFromInventory/{iid}/{pid}")]
        public async Task<ActionResult<string>> DeleteProductFromInventory(string iID, string pID)
        {

            byte[] error = new byte[500];
            int status = await _inventoryProductRepository.DeleteProductFromInventory(iID, pID, error);
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

        [HttpGet("Reporting")]
        public async Task<ActionResult<Report>> Reporting()
        {
            byte[] error = new byte[500];
            Report report = await _reportRepository.Reporting(error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(report);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("GetProductsMoves")]
        public async Task<ActionResult<List<ProductMoves>>> GetProductsMoves()
        {
            byte[] error = new byte[500];
            List<ProductMoves> products = await _productMovesRepository.GetProductsMoves(error);
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

        [HttpGet("ShowInventories")]
        public async Task<ActionResult<List<Inventory>>> ShowInventories()
        {
            byte[] error = new byte[500];
            List<Inventory> inventories = await _inventoryRepository.GetAll(error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(inventories);
            }
            else
            {
                return BadRequest(y);
            }
        }

        [HttpGet("ShowProductsInInventory/{id}")]
        public async Task<ActionResult<List<ProductInInventory>>> ShowProductsInInventory(string id)
        {
            byte[] error = new byte[500];
            List<ProductInInventory> products = await _inventoryProductRepository.ShowProductsInInventory(id, error);
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
      
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("ShowAvailableProducts")]
        public async Task<ActionResult<List<Product>>> ShowAvailableProducts()
        {
            byte[] error = new byte[500];
            List<Product> products = await _iProductRepository.ShowAvailableProducts(error);
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

        [HttpGet("ShowDeliveries")]
        public async Task<ActionResult<List<Order>>> ShowAllOrders()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowAllOrders(error);
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

        [HttpGet("ShowReceipts")]
        public async Task<ActionResult<List<Order>>> ShowReceipts()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowReceipts(error);
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

        [HttpGet("ShowCompletedReceipts")]
        public async Task<ActionResult<List<Order>>> ShowCompletedReceipts()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowCompletedReceipts(error);
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

        [HttpGet("ShowWaitingOrders")]
        public async Task<ActionResult<List<Order>>> ShowWaitingOrders()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowWaitingOrders(error);
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

        [HttpGet("ShowWaitingReceipts")]
        public async Task<ActionResult<List<Order>>> ShowWaitingReceiprs()
        {
            byte[] error = new byte[500];
            List<Order> orders = await _orderRepository.ShowWaitingReceipts(error);
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

        [HttpGet("SearchProducts/category/{value}")]
        public async Task<ActionResult<List<Product>>> SearchByCategory(string value)
        {
            byte[] error = new byte[500];
            List<Product> products = await _iProductRepository.SearchByCategory(value, error);
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

        [HttpGet("SearchInventories/{key}/{value}")]
        public async Task<ActionResult<List<Product>>> SearchInventories(string key, string value)
        {
            byte[] error = new byte[500];
            List<Inventory> inventories = await _inventoryRepository.SearchInventories(key, value, error);
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return Ok(inventories);
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
