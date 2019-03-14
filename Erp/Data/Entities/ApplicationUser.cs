using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp.Data
{
    public class ApplicationUser : IdentityUser
    {
              
        [Required]
        [MaxLength(250)]        
        public String DatabaseName { get; set; }        
        [Required]
        public String Country { get; set; }
        [Required]
        public String Language { get; set; }       
    }
}
