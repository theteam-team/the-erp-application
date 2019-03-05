using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class Numbers
    {
        [Required]
        public int N1 { get; set; }
        [Required]
        public int N2 { get; set; }
    }
}
