using Erp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;
using System.Threading;

namespace Erp
{
    /// <summary>
    /// Contains Common Properties and Methods needed For each http request
    /// </summary>
    public class CommonNeeds
    {

        /// <summary>
        private static Dictionary<int, string> _workItems = new Dictionary<int, string>();
        private static SemaphoreSlim _signal = new SemaphoreSlim(0);
        public  void QueueExection(int ThreadId, string workItem)
        {
         
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.TryAdd(ThreadId ,workItem);
            //_signal.Release();
        }

        public  async Task<string> DequeueAsync(int ThreadId)
        {
            //await _signal.WaitAsync();
          
            return _workItems[ThreadId];
        }

    }
}
