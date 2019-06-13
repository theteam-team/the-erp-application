using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data
{
    public class NotificationResponses
    {
        [Key]
        public long Id { get; set; }
        public string Response { get; set; }
        public long NotificationId { get; set; }
        public Notification Notification { get; set; }




    }
}
