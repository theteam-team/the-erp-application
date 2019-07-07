using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class TimedService : IHostedService
    {
        private ILogger<TimedService> _logger;
        private Timer _timer;

        public TimedService(ILogger<TimedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Started");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            
            return Task.CompletedTask;
           
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Doing Work");
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Closed");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;

        }
    }
}
