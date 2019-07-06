using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Customer
    {
        [Required]
        public string customer_id { get; set; } = "";
        public string name { get; set; } = "";
        public uint phone_number { get; set; } = 0;
        public string email { get; set; } = "";
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string DateOfBirth ;
        public string gender { get; set; } = "";
        public uint loyality_points ;
        public uint type;
        public string company { get; set; } = "";
        public string company_email { get; set; } = "";
        public bool is_lead { get; set; } = false;
    }
}
