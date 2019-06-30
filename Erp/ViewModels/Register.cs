using Erp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class Register
    {

        [Required]
        public string DatabaseName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Role { get; set; }

       
    }
}
