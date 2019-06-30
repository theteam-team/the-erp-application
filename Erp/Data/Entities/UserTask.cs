using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class UserTask
    {
        [Key]
        public string Id { get; set; }
        [JsonIgnore]
        public bool IsBpmEngine { get; set; }
        [JsonIgnore]
        public string InvokerID{ get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string ApplicationRoleId { get; set; }
        [JsonIgnore]
        public ApplicationRole ApplicationRole { get; set; }
        [JsonIgnore]
        public string  ApplicationUserId{ get; set; }
        [JsonIgnore]
        public ApplicationUser  ApplicationUser{ get; set; }
        [JsonIgnore]
        public bool IsDone { get; set; } = false;
       
        public List<UserTaskParameters> UserTaskParameters { get; set; }

    }
}
