using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Inventory
    {
        [Required]
        public string id;
        public string governorate;
        public string city;
        public string street;
        public double length;
        public double width;
        public double height;
    }
}
