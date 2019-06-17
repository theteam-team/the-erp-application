using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class WorkflowInstance
    {
        [Key]
        public string InstanceId { get; set; }
        public string CurrentNodeId { get; set; }

        public NodeLangWorkflow NodeLangWorkflow{ get; set; }

    }
}
