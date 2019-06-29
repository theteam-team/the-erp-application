using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class CustomerRegister
    {
        public string name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public uint phoneNumber{ get; set; }

        public string DateOfBirth;

        public string gender;

     
      
      
        
    }
}
