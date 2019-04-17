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
        public string name;      
        public uint phone_number;
        [EmailAddress]
        public string email;
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string DateOfBirth;
        public string gender;
        public uint loyality_points;
        public uint type;
        public string company;
        public string company_email;
        public bool is_lead;
    }
}
