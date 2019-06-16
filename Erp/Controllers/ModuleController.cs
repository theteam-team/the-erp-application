﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Erp.Data.Entities;

namespace Erp.Controllers
{
    /// <summary>
    /// Trival Class used for testing
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer, Identity.Application")]
    public class ModuleController : ControllerBase
    {        
        private AccountDbContext _accountDbcontext;
        private DataDbContext _dataDbContext;

        public ModuleController(DataDbContext dataDbContext, AccountDbContext accountsDbContext)
        {
            _accountDbcontext = accountsDbContext;
            _dataDbContext = dataDbContext;
        }
        
        
        [HttpGet("GetModules/{database}")]
        public ActionResult<List<Modules>> GetModules(string database)
        {
            Console.Write("ads");
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