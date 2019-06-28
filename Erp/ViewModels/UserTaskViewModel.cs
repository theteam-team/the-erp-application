using Erp.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class UserTaskViewModel
    {
        public string Title { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public bool IsBpmEngine { get; set; }
        [JsonIgnore]
        public string InvokerId { get; set; }

        public List<UserTaskParameters> UserTaskParameters { get; set; }
    }
}
