using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Invoice
    {
        
        public string suppId;
        public string suppName;
        public uint suppPhone;
        public string suppMail;
        //public string payment_method;
        public string productName;
        public double productCost;
        public uint suppUnits;
        public double totalCost;
        public double totalPaid;
        public double debts;
    }
}
