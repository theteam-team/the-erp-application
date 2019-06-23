using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Report

    {
        public uint deliveriesCycleTime;
        public uint receiptsCycleTime;
        public double inventoryValue;
        public double outgoingValue;
        public double incomingValue;
        public double inventoryTurnover;
    }
}
