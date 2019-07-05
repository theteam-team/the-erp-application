using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Erp.BackgroundServices.Entities;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Erp.Data.Entities;
using Erp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Erp.BackgroundServices
{
    public class BpmPollingService : BackgroundService
    {
        private IConfiguration _config;
        private TaskExectionQueue _executionQueue;
        private ILogger<BpmPollingService> _ilogger;
        private IServiceProvider _services;

        public BpmPollingService(IConfiguration config,TaskExectionQueue exectionQueue ,IServiceProvider services, ILogger<BpmPollingService> ilogger) 
        {
            _config = config;
            _executionQueue = exectionQueue;
            _ilogger = ilogger;
            _services = services;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ilogger.LogInformation("polling bpm");
            while (!stoppingToken.IsCancellationRequested)
            {
               await Task.Run(() =>
                {
                    pollWork();
                });

                Thread.Sleep(4000);
            }
        }

        public async void pollWork()
        {
            
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync(_config["BpmEngine:Address"] +"/engine/api/tasks");
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        List<BpmTask> bpmTasks = JsonConvert.DeserializeObject<List<BpmTask>>(content);                     
                       _ilogger.LogInformation(content);
                        if (bpmTasks.Count > 0)
                        {
                            foreach (var item in bpmTasks)
                            {
                                using (var scope = _services.CreateScope())
                                {
                                    var _accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();

                                    var bpmWorker = new BpmWorker
                                    {
                                        instanceID = item.instanceID,
                                        workflowName = item.workflowName,
                                        taskID = item.taskID
                                    };
                                    _accountDbContext.BpmWorkers.Add(bpmWorker);
                                    _accountDbContext.SaveChanges();
                                    item.InvokerId = bpmWorker.Id;

                                    item.databaseName = _accountDbContext.Organizations.FirstOrDefault().Name;//((ClaimsIdentity)HttpContext.User.Identity).FindFirst("organization").Value;
                                    _ilogger.LogInformation(item.databaseName);
                                    if (item != null)
                                    {
                                        item.IsBpm = true;
                                        _executionQueue.QueueExection(item);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception EX)
                {

                    //_ilogger.LogInformation(EX.Message);
                }
                

            }
        }
    }
}
