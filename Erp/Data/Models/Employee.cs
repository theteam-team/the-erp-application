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
    public class Employee
    {
        [Required]
        public string id;
        public string first_name;
        public string middle_name;
        public string last_name;
        public string email;
        public uint phone_number;
        public uint year_birth;
        public uint month_birth;
        public uint day_birth;
        public string gender;
        public uint points;
        public bool is_available;
        public string role;
        public string department;
    }
}
