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

namespace Erp.BackgroundServices
{
    public class BpmPollingService : BackgroundService
    {
        private TaskExectionQueue _exectionQueue;
        private ILogger<BpmPollingService> _ilogger;
        private IServiceProvider _services;

        public BpmPollingService(TaskExectionQueue exectionQueue ,IServiceProvider services, ILogger<BpmPollingService> ilogger) 
        {
            _exectionQueue = exectionQueue;
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
                    var result = await client.GetAsync("http://102.187.45.214/engine/api/tasks");
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
                                    if (item != null)
                                    {
                                        item.IsBpm = true;
                                        _exectionQueue.QueueExection(item);
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
