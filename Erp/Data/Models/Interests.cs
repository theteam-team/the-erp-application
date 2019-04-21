using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Interests
    {
        public uint interest_id;
        public string customer_id;
    }
}
