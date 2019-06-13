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
        public _Email Email { get; set; }
        [Required]
        public _EmailType EmailType { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
