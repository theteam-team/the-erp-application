﻿using System;
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

        public AccountingApiController(IProductRepository iProductRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _iProductRepository = iProductRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }

        [HttpGet("GetSoldProduct")]
        public async Task<ActionResult<List<Product>>> GetSoldProduct()
        {
            byte[] error = new byte[500];
            List<Product> products = await _iProductRepository.getSoldProduct(error);
          
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
    }
}