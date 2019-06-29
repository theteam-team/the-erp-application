using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Erp.Database_Builder;
using Microsoft.AspNetCore.Authorization;
using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using System.Security.Claims;
using Erp.Data.Entities;
using Erp.Data;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private AccountDbContext _accountDbContext;
        private ModulesDatabaseBuilder _databaseBuilder;
        private TaskExectionQueue _exectionQueue;

        public TestController(AccountDbContext accounDbContext , ModulesDatabaseBuilder databaseBuilder, TaskExectionQueue exectionQueue)
        {
            _accountDbContext = accounDbContext;
            _databaseBuilder = databaseBuilder;
            _exectionQueue = exectionQueue;
        }
        [HttpGet("CreateDatabase")]
        public ActionResult CreateDatabase(string database)
        {
            _databaseBuilder.createModulesDatabase(database);
            return Ok();
        }
        [HttpPost("RunTask")]
        public ActionResult RunTask(BpmTask bpmTask)
        {
            var bpmWorker = new BpmWorker
            {
                instanceID = bpmTask.instanceID,
                WorkflowName = bpmTask.WorkflowName,
                taskID = bpmTask.taskID
            };
            _accountDbContext.BpmWorkers.Add(bpmWorker);
            _accountDbContext.SaveChanges();
            bpmTask.InvokerId = bpmWorker.Id;

            bpmTask.databaseName = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("database").Value;
            _exectionQueue.QueueExection(bpmTask);
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
