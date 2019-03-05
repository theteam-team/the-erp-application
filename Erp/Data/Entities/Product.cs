using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Erp.Data
{
    public class Product
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
