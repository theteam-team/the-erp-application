using Erp.BackgroundServices.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class BpmInvokerService : BackgroundService
    {
        private BpmInvokerQueue _invokerQueue;
        private IConfiguration _config;
        private ILogger<BpmInvokerService> _logger;

        public BpmInvokerService(BpmInvokerQueue invokerQueue, ILogger<BpmInvokerService> logger  ,IConfiguration config)
        {
            _invokerQueue = invokerQueue;
            _config = config;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Bpm Invoker Service is Starting");
            while (!stoppingToken.IsCancellationRequested)
            {
                BpmWorkFlow bpmWorkFlow = await _invokerQueue.DequeueAsync(stoppingToken);
                if (bpmWorkFlow != null)
                {
                    await invokeWorkFlow(bpmWorkFlow);
                }
               
            }
            _logger.LogInformation("Bpm Invoker Service is Stoping");

        }

        private async Task invokeWorkFlow(BpmWorkFlow bpmWorkFlow)
        {
            _logger.LogInformation("Bpm Invoker Service Invoking Workflow "+ bpmWorkFlow.workflowName);
            using (var client = new HttpClient() )
            {
                try
                {
                    string url = _config["BpmEngine:Address"] + "/engine/api/workflow?name=";
                    _logger.LogInformation("Sending to  " + url);
                    await client.PostAsJsonAsync(url + bpmWorkFlow.workflowName, bpmWorkFlow.Param);

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

    }
}
