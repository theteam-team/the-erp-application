using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class Notification
    {
        [Key]
        public long Id { get; set; }

        public string message { get; set; }

        public IList<NotificationApplicationUser> NotificationApplicationUser { get; set; }
    }
}
