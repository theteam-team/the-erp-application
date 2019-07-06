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
    public class AnInvoice
    {
        [Required]
        public string supplied_department;
        public string employee_ID;
        public string employee_name;
        public string  Supplier_Name;
        public string Supplier_ID;
        public string Supplier_Email;
        public string Supplier_Phone_Number;
        public string product_id;
        public int Units_Supplied;
        public double product_cost;
        
    }
}
