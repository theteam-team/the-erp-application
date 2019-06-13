using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data
{
    public class Notification
    {
        [Key]
        public long Id { get; set; }

        public string message { get; set; }

        public string NotificationType { get; set; }

        public IList<NotificationResponses> NotificationResponses { get; set; }

        public IList<NotificationApplicationUser> NotificationApplicationUsers { get; set; }
    }
}
