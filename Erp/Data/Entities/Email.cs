using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class Email
    {
        [Key]
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
        [JsonIgnore]

        [EmailAddress]
        public string recieverEmail{ get; set; }
        [StringLength(50)]
        public string recieverName{ get; set; }
        
    }
}
