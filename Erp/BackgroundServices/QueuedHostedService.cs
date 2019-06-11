using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
            ILoggerFactory loggerFactory)
        {
            TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<QueuedHostedService>();
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Hosted Service is starting.");
            

                foo();
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);
                try
                {
                    //_logger.LogInformation("Queued Hosted Service is Working.");
                    await workItem(cancellationToken);
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                       $"Error occurred executing {nameof(workItem)}.");
                }
            }

            _logger.LogInformation("Queued Hosted Service is stopping.");
        }


        protected Task foo()
        {
            Task[] tasks = new Task[5];
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(() => {
                    int TaskId = (int)Task.CurrentId;
                    // Each task begins by requesting the semaphore.
                    Console.WriteLine("Task {0} begins and waits for the semaphore.",
                                      Task.CurrentId);
                    Func<CancellationToken, Task> func = _cancellationToken =>
                    {
                        Console.WriteLine("Task {0}", TaskId);
                        return Task.CompletedTask;
                    };


                    TaskQueue.QueueBackgroundWorkItem(func);


                    //Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

                    // The task just sleeps for 1+ seconds.



                });
            }
            return Task.CompletedTask;

        }
    }
}
