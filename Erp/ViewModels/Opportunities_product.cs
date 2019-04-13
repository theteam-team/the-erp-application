using Erp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.ViewModels.CRN_Tabels
{

    public class Opportunities_product
    {
        [Required]
        public Opportunity Opportunities { get; set; }
        [Required]
        public string[] product_id { get; set; }
    }   
}
