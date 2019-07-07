using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using Erp.Interfaces;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureApiController : ControllerBase
    {
        public IProductRepository _productRepository { get; }

        public IOrderProductRepository _orderProductRepository { get; }
        public BpmInvokerQueue _bpmInvokerQueue { get; }

        public ManufactureApiController(BpmInvokerQueue bpmInvokerQueue, IOrderProductRepository  orderProductRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _bpmInvokerQueue = bpmInvokerQueue;
            _orderProductRepository = orderProductRepository;
        }
        [HttpPost("InvokeOrder")]
        public async Task<ActionResult> InvokeOrder(Order order)
        {
            List<Task> tasks = new List<Task>();
            byte[] error = new byte[500];
            List<ProductInOrder> productInOrders = await _orderProductRepository.ShowProductsInOrder(order.id, error);
            List<Product> products = new List<Product>();
            if (checkQuery(error))
            {
                BpmWorkFlow bpmWorkFlow = new BpmWorkFlow();
                JObject jObject = new JObject();
                bpmWorkFlow.workflowName = "node";

                foreach (var productInOrder in productInOrders)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        byte[] err = new byte[500];
                        Console.WriteLine("pid = " + productInOrder.productID);
                        Product product = await _productRepository.GetById(productInOrder.productID, err);
                        if (checkQuery(err))
                        {
                            products.Add(product);
                            jObject.Add(product.name, (productInOrder.unitsOrdered.ToString()));
                        }
                    }));
                }
                await Task.WhenAll(tasks);
                bpmWorkFlow.Param = jObject;
                _bpmInvokerQueue.QueueExection(bpmWorkFlow);
                return Ok("successfully Invoke");
            }
            else
            {
                return BadRequest("Error");
            }
        }

        private bool checkQuery(byte[] error)
        {
            
            string z = Encoding.ASCII.GetString(error);
            string y = z.Remove(z.IndexOf('\0'));
            if (y == "")
            {

                return true;
            }
            else
            {
                Console.WriteLine(y);
                return false;
            }
        }
    }
}