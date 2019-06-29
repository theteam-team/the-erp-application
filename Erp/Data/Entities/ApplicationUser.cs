
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp.Data.Entities
{
    ///<summary >   
    /// Represents The user Tabel in the Database, each proberty represents a column
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
              
        [Required]
        [MaxLength(250)]        
        public string DatabaseName { get; set; }        
        
        public string Country { get; set; }
      
        public string Language { get; set; }    
        
        public IList<UserHasEmail> Emails { get; set; }

        public IList<UserTask> UserTasks{ get; set; }
        public string OrganizationId{ get; set; }
        public Organization Organization{ get; set; }

    }
}
