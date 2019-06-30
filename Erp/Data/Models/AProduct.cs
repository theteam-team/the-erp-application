using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class AProduct
    {
        public string id;
        public string name;
        public uint unitsInOrder;
        public double price;
        public double totalPrice;
    }
}

