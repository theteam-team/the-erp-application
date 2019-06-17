using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class ProductSold
    {
            public string Productid;
            public uint unitsSold;
            public double cost;
            public double price;
            public double profit;
    }
}
