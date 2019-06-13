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
        [Required]
        public string name;
        public string description;
        [Required]
        public string position;
        [Required]
        public double price;
        [Required]
        public double size;
        public double weight;
        [Required]
        public uint unitsInStock;


    }
}
