using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Product
    {
        [Required]
        public string id;
        public string name;
        public string description;
        public string position;
        public double price;
        public double size;
        public double weight;
        public uint unitsInStock;
    }
}
