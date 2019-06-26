using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.BackgroundServices.Entities
{
    public class BpmTask
    {
        [JsonIgnore]
        public string databaseName { get; set; }
        [JsonIgnore]
        public string Invoker { get; set; }
        public string Type { get; set; }
        public string WorkflowName { get; set; }
        public string instanceID { get; set; }
        public string taskID { get; set; }
        public string TaskName { get; set; }   
        public object[] TaskParam { get; set; }
    }
}
