using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class ProcRequest
    {
        [Key]
        public string Name { get; set; }
        public string Discription { get; set; }
        public string BpmWorkflowName { get; set; } = null;      
        public List<BpmWorkflowParameters> WorkflowParameters { get; set; }


    }
}
