using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private AccountsDbContext _accountDbcontext;
        private DataDbContext _dataDbContext;

        public ModuleController(DataDbContext dataDbContext, AccountsDbContext accountsDbContext)
        {
            _accountDbcontext = accountsDbContext;
            _dataDbContext = dataDbContext;
        }
        [HttpGet("GetModules/{database}")]
        public ActionResult<List<Modules>> GetModules(string database)
        {
            if (!CommonNeeds.checkdtb(_dataDbContext, database))
            {

                return BadRequest("No Database with this specifc name exist is created");
            }
            return _dataDbContext.Modules.ToList();
            
        }

        [HttpGet("GetModules/{id}/{database}")]
        public async Task<ActionResult<Modules>> GetModules(int id, string database)
        {
            if (!CommonNeeds.checkdtb(_dataDbContext, database))
            {

                return BadRequest("No Database with this specifc name exist is created");
            }
            var module = await _dataDbContext.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return module;
            
        }

        [HttpPost("AddModule/{database}")]
        public ActionResult<Modules> AddModule(Modules module, string database)
        {
            if (!CommonNeeds.checkdtb(_dataDbContext, database))
            {
                return BadRequest("No Database with this specifc name exist is created");
            }
            
            _dataDbContext.Modules.Add(module);

            _dataDbContext.SaveChanges();
           

            return CreatedAtAction(nameof(GetModules), new { id = module.Id, database = database }, module);
        }
    }
}