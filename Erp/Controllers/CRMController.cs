using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data;
using Erp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Erp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CRMController : ControllerBase
    {
        private DataDbContext _dataDbContext;

        public CRMController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
          
        }
        [HttpPost("AddNumbers")]
        public ActionResult<int> AddNumbers(Numbers numbers)
        {
            return Dll_Wrapper.AddNumbers(numbers.N1, numbers.N2) ;
        }

        // GET api/<controller>
        [HttpPost("MultiplyNumbers")]
        public ActionResult<int> MultiplyNumbers(Numbers numbers)
        {
            return Dll_Wrapper.MultiplyNumbers(numbers.N1, numbers.N1);
        }

        
        

        
    }
}
