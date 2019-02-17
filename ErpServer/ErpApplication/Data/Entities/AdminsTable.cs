using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpApplication.Data
{
    public class AdminsTable : IdentityUser
    {
        /// <summary>
        /// The primary key
        /// </summary>        
        [Required]
        [MaxLength(250)]        
        public String DatabaseName { get; set; }        
        [Required]
        public String Country { get; set; }
        [Required]
        public String Language { get; set; }
    }
}
