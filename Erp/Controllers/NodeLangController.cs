using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Controllers
{
    public class NodeLangController : Controller
    {
        [HttpGet("monitoring")]
        public IActionResult Monitoring()
        {
            return View();
        }
    }
}