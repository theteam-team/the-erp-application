using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data
{
    public class Email
    {
        [Key]
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
        [JsonIgnore]
        public IList<UserHasEmail> UserHasEmails { get; set; }
    }
}
