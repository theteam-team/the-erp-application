using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class BpmWorker
    {
        [Key]
        public string Id { get; set; }
        public string workflowName { get; set; }
        public string instanceID { get; set; }
        public string taskID { get; set; }
    }
}
