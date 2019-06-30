using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Out
    {
        [Required]
        public string payment_id;
        public string payment_method;
        //public string payment_date;
        public double payment_amount;
    }
}
