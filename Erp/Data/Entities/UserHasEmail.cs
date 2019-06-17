using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class UserHasEmail
    {
        public string EmailId { get; set; }
        public Email Email { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string EmailTypeId { get; set; }
        public EmailType EmailType { get; set; }

        public bool IsSent { get; set; } = false;
    }
}
