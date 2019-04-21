using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Customer_interests
    {
        public uint interest_id;
        public string customer_id;
        public uint level_of_interest;
    }
}
