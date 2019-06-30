using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data.Entities
{
    public class UserTaskParameters
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int userTaskId { get; set; }
        [JsonIgnore]
        public UserTask userTask { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public string Value { get; set; } = null;

    }
}
