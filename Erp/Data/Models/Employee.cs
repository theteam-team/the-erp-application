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
        public string name;       
        public uint phone_number;
        [EmailAddress]
        public string email;       
        public string Dateofbirth;
        public string gender;
        public uint points;
        public bool is_available;
        public string role;
        public string department;
    }
}
