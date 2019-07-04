using Erp.BackgroundServices.Entities;
using Erp.Data;
using Erp.Interfaces;
using Erp.Microservices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class TaskExecutionEngine : BackgroundService
    {
        private readonly ILogger<TaskExecutionEngine> _logger;
        
        private TaskExectionQueue _taskExectionQueue;
        public List<Task> tasks = new List<Task>();
        public IServiceProvider Services { get; }

        public TaskExecutionEngine(
            ILoggerFactory loggerFactory, IServiceProvider services, TaskExectionQueue taskExectionQueue)
        {
            _taskExectionQueue = taskExectionQueue;           
            Services = services;               
            _logger = loggerFactory.CreateLogger<TaskExecutionEngine>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Task Execution Engine Hosted Service is starting.");

            
            while (!stoppingToken.IsCancellationRequested)
            {

                BpmTask bpmTask = await _taskExectionQueue.DequeueAsync(stoppingToken);
                if (bpmTask != null)
                {
                    tasks.Add(Execute(bpmTask));                   
                }
                //Thread.Sleep(1000);
            }

            await Task.WhenAll(tasks);

            _logger.LogInformation("Task Execution Engine Hosted Service is stopping.");
           
        }

      
        public async Task Execute(BpmTask bpmTask)
        {
                _logger.LogInformation("executing " + bpmTask.TaskName);
           
                using (var scope = Services.CreateScope())
                {
                    var _sysServices = scope.ServiceProvider.GetRequiredService<SystemServices>();

                    await  _sysServices.findServiceAsync(bpmTask, scope);
                   
               }
            
        }
    }
}
