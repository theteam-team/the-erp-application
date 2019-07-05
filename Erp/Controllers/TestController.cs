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
        private BpmInvokerQueue _bpmInvokerQueue;
        private AccountDbContext _accountDbContext;
        private ModulesDatabaseBuilder _databaseBuilder;
        private TaskExectionQueue _exectionQueue;

        public TestController(BpmInvokerQueue bpmInvokerQueue,AccountDbContext accounDbContext , ModulesDatabaseBuilder databaseBuilder, TaskExectionQueue exectionQueue)
        {
            _bpmInvokerQueue = bpmInvokerQueue;
            _accountDbContext = accounDbContext;
            _databaseBuilder = databaseBuilder;
            _exectionQueue = exectionQueue;
        }
        [HttpGet("CreateDatabase")]
        public async Task<ActionResult> CreateDatabase(string database)
        {
            await _databaseBuilder.createModulesDatabaseAsync(database);
            return Ok();
        }
        [HttpPost("RunTask")]
        public ActionResult RunTask(BpmTask bpmTask)
        {
            var bpmWorker = new BpmWorker
            {
                instanceID = bpmTask.instanceID,
                workflowName = bpmTask.workflowName,
                taskID = bpmTask.taskID
            };
            _accountDbContext.BpmWorkers.Add(bpmWorker);
            _accountDbContext.SaveChanges();
            bpmTask.InvokerId = bpmWorker.Id;

            bpmTask.databaseName = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("organization").Value;
            bpmTask.IsBpm = true;
            _exectionQueue.QueueExection(bpmTask);
            return Ok();
        }

        [HttpGet("CreateNewModule")]
        public async Task<ActionResult> CreateNewModule(string moduleName, string database)
        {
           await _databaseBuilder.createNewModule(database, moduleName);
            return Ok();
        }
        [HttpPost("RunWorkFlow")]
        public ActionResult RunWorkFlow(BpmWorkFlow bpmWorkFlow)
        {
            _bpmInvokerQueue.QueueExection(bpmWorkFlow);
            return Ok();
        }

       
    }
}
