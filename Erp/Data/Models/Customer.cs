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
        public string customer_id;
        public string first_name;
        public string middle_name;
        public string last_name;
        [EmailAddress]
        public string email;
        public uint phone_number;
        public string gender;
        public uint loyality_points;
        public uint type;
        public uint year_birth;
        public uint month_birth;
        public uint day_birth;
        public string company;
        public string company_email;
        public bool is_lead;
    }
}
