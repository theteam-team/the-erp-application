using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Erp.ViewModels;
using Erp.Data.Entities;
using Erp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Erp.BackgroundServices;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ProcessRequstApiController : ControllerBase
    {
        private IProcRequestRepo _requestRepo;
        private IBmpParmRepo _parmRepo;
        private TaskExectionQueue _bmExectionQueue;

        public ProcessRequstApiController(IProcRequestRepo requestRepo, IBmpParmRepo parmRepo, TaskExectionQueue bmExectionQueue)
        {
            _requestRepo = requestRepo;
            _parmRepo = parmRepo;
            _bmExectionQueue = bmExectionQueue;
            
        }
        [HttpPost("AddRequest")]
        public async Task<ActionResult> AddRequest([FromBody] BpmRequestViewModel requestViewModel)
        {
            ProcRequest request = new ProcRequest()
            {
                Name = requestViewModel.RequestName,
                Discription = requestViewModel.Description,
                BpmWorkflowName = requestViewModel.WorkflowName,
                WorkflowParameters = requestViewModel.WorkflowParameters

            };
            await _requestRepo.Insert(request);
            return Ok("ok");

        }
        /*[HttpGet("RunRequest/{requestName}")]
        public async Task<ActionResult> RunRequest([FromRoute]string requestName)
        {
            _bmExectionQueue.QueueExection(requestName);
            return Ok("ok");

        }*/
        [HttpPost("UpdateRequest")]
        public async Task<ActionResult> RunRequest([FromBody]BpmRequestViewModel requestViewModel)
        {
            ProcRequest request = new ProcRequest()
            {
                Name = requestViewModel.RequestName,
                Discription = requestViewModel.Description,
                BpmWorkflowName = requestViewModel.WorkflowName,
                WorkflowParameters = requestViewModel.WorkflowParameters

            };
            await _requestRepo.Update(request);
            return Ok("ok");

        }
        

        
    }
}