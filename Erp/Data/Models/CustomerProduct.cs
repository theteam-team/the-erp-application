﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class CustomerProduct
    {
        public string customerID;
        public string orderID;
        public string productID;
        public string name;
        public uint unitsOrdered;
        public double price;
        public double total;
    }
}
