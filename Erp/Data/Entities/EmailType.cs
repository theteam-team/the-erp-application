using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class EmailType
    {
        [Key]
        public string Id { get; set; }

        public string  Type { get; set; }
        [JsonIgnore]
        public IList<UserHasEmail> UserHasEmails { get; set; }

    }
}
