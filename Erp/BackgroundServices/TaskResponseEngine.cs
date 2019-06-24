using Erp.BackgroundServices.Entities;
using Erp.Microservices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class TaskResponseEngine : BackgroundService
    {
        private readonly ILogger<TaskResponseEngine> _logger;

        private TaskResponseQueue _taskResponseQueue;
        public List<Task> tasks = new List<Task>();
        public IServiceProvider Services { get; }

        public TaskResponseEngine(
            ILoggerFactory loggerFactory, IServiceProvider services, TaskResponseQueue taskResponseQueue)
        {
            _taskResponseQueue = taskResponseQueue;
            Services = services;
            _logger = loggerFactory.CreateLogger<TaskResponseEngine>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Task Response Engine Hosted Service is starting.");


            while (!stoppingToken.IsCancellationRequested)
            {

                BpmResponse bpmResponse = await _taskResponseQueue.DequeueAsync(stoppingToken);
                if (bpmResponse != null)
                {
                    tasks.Add(Execute(bpmResponse));

                }

                Thread.Sleep(100);

            }

            _logger.LogInformation("Task Response Engine Hosted Service is stopping.");

        }


        public async Task Execute(BpmResponse bpmResponse)
        {
            _logger.LogInformation("Responsing " + bpmResponse.status);
 

            using (var scope = Services.CreateScope())
            {
                

                //To do find type and send


            }

        }
    }
}
