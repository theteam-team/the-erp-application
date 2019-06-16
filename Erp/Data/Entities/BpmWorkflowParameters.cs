using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class BpmWorkflowParameters
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
    }
}
