using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Erp.Database_Builder;
namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ModulesDatabaseBuilder _databaseBuilder;

        public TestController(ModulesDatabaseBuilder databaseBuilder)
        {
            _databaseBuilder = databaseBuilder;
        }
        [HttpGet("CreateDatabase")]
        public ActionResult CreateDatabase(string database)
        {
            _databaseBuilder.createModulesDatabase(database);
            return Ok();
        }
        [HttpGet("CreateNewModule")]
        public ActionResult CreateNewModule(string moduleName, string database)
        {
            _databaseBuilder.createNewModule(database, moduleName);
            return Ok();
        }
    }
}
