using Erp.Data;
using Erp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        public Email Email { get; set; }
        [Required]
        public string EmailTypeId { get; set; }
        [Required]
        public List<string> UserNames { get; set; }
    }
}
