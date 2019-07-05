using Erp.BackgroundServices.Entities;
using Erp.Data;
using Erp.Microservices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class TaskResponseEngine : BackgroundService
    {
        private readonly ILogger<TaskResponseEngine> _logger;
        private IConfiguration _config;
        private TaskResponseQueue _taskResponseQueue;
        public List<Task> tasks = new List<Task>();
        public IServiceProvider Services { get; }

        public TaskResponseEngine(IConfiguration config,
            ILoggerFactory loggerFactory, IServiceProvider services, TaskResponseQueue taskResponseQueue)
        {
            _config = config;
            _taskResponseQueue = taskResponseQueue;
            Services = services;
            _logger = loggerFactory.CreateLogger<TaskResponseEngine>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Task Response Engine Hosted Service is starting.");


            while (!stoppingToken.IsCancellationRequested)
            {

                ServiceResponse bpmResponse = await _taskResponseQueue.DequeueAsync(stoppingToken);
                if (bpmResponse != null)
                {
                    tasks.Add(Execute(bpmResponse));

                }

                Thread.Sleep(100);

            }

            _logger.LogInformation("Task Response Engine Hosted Service is stopping.");

        }


        public async Task Execute(ServiceResponse Response)
        {
            _logger.LogInformation("Responsing " + Response.status);
 
            
            using (var scope = Services.CreateScope())
            {
                
                if (Response.IsBpm)
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                    var bpmInfo = _context.BpmWorkers.Where(w => w.Id == Response.InvokerId).FirstOrDefault();
                    var bpmResponse = new BpmResponse()
                    {
                        status = Response.status,
                        instanceID = bpmInfo.instanceID,
                        taskID = bpmInfo.taskID,
                        workflowName = bpmInfo.workflowName,
                        type = Response.Type,
                        responseParam = Response.parameters

                    };
                    string json = JsonConvert.SerializeObject(bpmResponse);
                    //_logger.LogInformation("sending json" + json);
                    using (var client = new HttpClient())
                    {
                        _logger.LogInformation("sending json" + json);
                        var content = await client.PostAsJsonAsync(_config["BpmEngine:Address"] + "/engine/api/notification", bpmResponse);
                    }
                }
                else
                {

                }
                

                //To do find type and send


            }

        }
    }
}
