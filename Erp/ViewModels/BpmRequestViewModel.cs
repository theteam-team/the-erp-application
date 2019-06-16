using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data.Entities;
namespace Erp.ViewModels
{
    public class BpmRequestViewModel
    {
       
        public string RequestName { get; set; }     
        public string WorkflowName { get; set; }
        public string Description { get; set; }

        public List<BpmWorkflowParameters> WorkflowParameters { get; set; }
    }
}
