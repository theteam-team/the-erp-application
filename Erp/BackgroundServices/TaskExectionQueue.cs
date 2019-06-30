using Erp.BackgroundServices.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class TaskExectionQueue
    {
        private ConcurrentQueue<BpmTask> _workItems = new ConcurrentQueue<BpmTask>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public void QueueExection(BpmTask workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<BpmTask> DequeueAsync(CancellationToken cancellationToken) {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

        
   
    }
}
