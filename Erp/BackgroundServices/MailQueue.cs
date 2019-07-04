using Erp.BackgroundServices.Entities;
using Erp.Data.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class MailQueue
    {
        private ConcurrentQueue<Email> _workItems = new ConcurrentQueue<Email>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);
        public void QueueExection(Email workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Email> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
