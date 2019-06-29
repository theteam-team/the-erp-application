using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class Organization
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<ApplicationUser> users { get; set; }
    }
}
