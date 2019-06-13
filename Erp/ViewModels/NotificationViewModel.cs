using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class NotificationViewModel
    {
        public string  Message{ get; set; }
        public IList<string> Responses{ get; set; }
        public string UserID{ get; set; }
    }
}
