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
        public bool IsBpm { get; set; }
        [JsonIgnore]
        public string InvokerId { get; set; }
        public string type { get; set; }
        public string workflowName { get; set; }
        public string instanceID { get; set; }
        public string taskID { get; set; }
        public string TaskName { get; set; }   
        public object[] TaskParam { get; set; }
    }
}
