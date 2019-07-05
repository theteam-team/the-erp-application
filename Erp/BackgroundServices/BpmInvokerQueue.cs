using Erp.BackgroundServices.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class BpmInvokerQueue
    {
        private ConcurrentQueue<BpmWorkFlow> _workItems = new ConcurrentQueue<BpmWorkFlow>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public void QueueExection(BpmWorkFlow workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<BpmWorkFlow> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
