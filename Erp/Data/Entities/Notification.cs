using Newtonsoft.Json;
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
        [JsonIgnore]
        [Key]
        public long Id { get; set; }

        public string message { get; set; }

      
        public string NotificationType { get; set; }
        public string EntityID  { get; set; }



        public string ApplicationUserId { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser{ get; set; }
    }
}
