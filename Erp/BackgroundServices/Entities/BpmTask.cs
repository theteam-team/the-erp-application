using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.BackgroundServices.Entities
{
    public class BpmTask
    {
        public string TaskId { get; set; }
        public string TaskType { get; set; }
        public string TaskName { get; set; }
        public string TaskParam { get; set; }
    }
}
