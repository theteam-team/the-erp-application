using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Erp.Data
{
    public class Modules
    {
        [Required]
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Discription{ get; set; }
        [Required]
        public string Price{ get; set; }
        

    }
}
