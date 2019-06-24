using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.BackgroundServices.Entities
{
    public class BpmResponse
    {
        [JsonIgnore]
        public string databaseName  { get; set; }
        public string Type { get; set; }
        public string WorkflowName { get; set; }
        public string instanceID { get; set; }
        public string taskID { get; set; }
        public string status { get; set; }
        public object ResponseParam { get; set; }

    }
}
