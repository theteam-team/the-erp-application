using Erp.BackgroundServices;
using Erp.BackgroundServices.Entities;
using Erp.Controllers;
using Erp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Microservices
{
    public class SystemServices
    {
        private CrmApiController _crmApiController;
        private TaskResponseQueue _responseQueue;

        public SystemServices(CrmApiController crmApiController, TaskResponseQueue responseQueue)
        {
            _crmApiController = crmApiController;
            _responseQueue = responseQueue;
        }
        public async void addCustomer(BpmTask bpmTask)
        {
            Console.WriteLine("Hereee");

            string json = ((JArray)bpmTask.TaskParam).ToString(Formatting.None);
            Customer customer = JsonConvert.DeserializeObject<List<Customer>>(json)[0];
            _crmApiController._customeRepository.setConnectionString(bpmTask.databaseName);
            var result = await _crmApiController.AddCustomer(customer);
            try
            {
                OkObjectResult x = (OkObjectResult)result.Result;
                BpmResponse bpmResponse = new BpmResponse
                {
                    databaseName = bpmTask.databaseName,
                    status = "success",
                    WorkflowName = bpmTask.WorkflowName,
                    instanceID = bpmTask.instanceID,
                    taskID = bpmTask.taskID,
                    Type = bpmTask.Type,
                    ResponseParam = x.Value

                };
                _responseQueue.QueueExection(bpmResponse);
                Console.WriteLine(x.StatusCode);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                BpmResponse bpmResponse = new BpmResponse
                {
                    databaseName = bpmTask.databaseName,
                    status = "Failed",
                    WorkflowName = bpmTask.WorkflowName,
                    instanceID = bpmTask.instanceID,
                    taskID = bpmTask.taskID,
                    Type = bpmTask.Type,
                  

                };
                _responseQueue.QueueExection(bpmResponse);           
            }

        }

    }
}
