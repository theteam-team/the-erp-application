using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class NotificationViewModel
    {
        public string  Message{ get; set; }
        public List<string> UserNames{ get; set; }
        public string  NotificationType{ get; set; }
        public IList<string> Responses{ get; set; }
    }
}
