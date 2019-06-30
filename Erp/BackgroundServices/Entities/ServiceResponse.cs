using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.BackgroundServices.Entities
{
    public class ServiceResponse
    {
        public bool IsBpm{ get; set; }
        public string InvokerId{ get; set; }
        public string Organization{ get; set; }
        public string status { get; set; }
        public string Type { get; set; }
        public object parameters { get; set; }
    }
}
