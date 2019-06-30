using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.BackgroundServices.Entities
{
    public class BpmResponse
    {
      
        public string type { get; set; }
        public string workflowName { get; set; }
        public string instanceID { get; set; }
        public string taskID { get; set; }
        public string status { get; set; }
        public object responseParam { get; set; }

    }
}
