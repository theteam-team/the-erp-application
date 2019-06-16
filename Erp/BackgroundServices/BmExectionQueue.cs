using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class BmExectionQueue
    {
        private ConcurrentQueue<string> _workItems = new ConcurrentQueue<string>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public void QueueExection(string workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<string> DequeueAsync(CancellationToken cancellationToken)  {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
