using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class NotificationResponses
    {
        [Key]
        [JsonIgnore]
        public long Id { get; set; }
        public string Response { get; set; }
        [JsonIgnore]
        public long NotificationId { get; set; }
        [JsonIgnore]
        public Notification Notification { get; set; }




    }
}
