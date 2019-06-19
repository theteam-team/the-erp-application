using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class ProductInInventory
    {
        [Required]
        public string inventoryID;
        public string productID;
        public string name;
        public string position;
        public double weight;
        public double length;
        public double width;
        public double height;
        public uint unitsInInventory;
    }
}
