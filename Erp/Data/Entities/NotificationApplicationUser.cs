using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class NotificationApplicationUser
    {
        public long NotificationId { get; set; }
        public Notification Notification { get; set;}

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }      
        
       



    }
}
