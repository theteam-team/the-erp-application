using Erp.Data;
using Erp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class BmService : BackgroundService
    {
        private readonly ILogger<BmService> _logger;
        private Timer _timer;
        private BmExectionQueue _bmExectionQueue;

        public IServiceProvider Services { get; }

        public BmService(
            ILoggerFactory loggerFactory, IServiceProvider services, BmExectionQueue bmExectionQueue)
        {
            _bmExectionQueue = bmExectionQueue;
            //_email = email;
            Services = services;
            //_emailUser = emailUser;


            //TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<BmService>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Bpm Hosted Service is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {

                await loadWork(stoppingToken);
                Thread.Sleep(2000);

            }

            _logger.LogInformation("Bpm Hosted Service is stopping.");
            

        }
        async Task loadWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var _paramRepo = scope.ServiceProvider.GetRequiredService<IBmpParmRepo>();
                var _requsetRepo = scope.ServiceProvider.GetRequiredService<IProcRequestRepo>();
                var _accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                string request = await _bmExectionQueue.DequeueAsync(stoppingToken);
                if (request != null)
                {
                    var workflow = await _requsetRepo.GetById(request);
                    var workflowName = workflow.BpmWorkflowName;
                    using (var _httpclient = new HttpClient())
                    {
                        Console.WriteLine("here");
                        try
                        {
                            var x = JsonConvert.SerializeObject(workflow.WorkflowParameters, Formatting.Indented);
                            _logger.LogInformation("sending to " + "http://localhost:8090/engine/api/workflow?name=" + workflowName + " data : " + x);
                            await _httpclient.PostAsJsonAsync("http://localhost:8090/engine/api/workflow?name=" + workflowName, workflow.WorkflowParameters);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            
                        }
                        
                    }
                }

            }
        }
    }
}
