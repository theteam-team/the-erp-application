using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class NodeLang_Workflow
    {
        [Key]
        public Guid Id { get; set; }
        public string MyProperty { get; set; }
        //public N MyProperty { get; set; }
    }
}
