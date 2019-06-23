using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class ProductMoves
    {
        public string time;
        public string id;
        public string name;
        public string status;
        public uint quantity;
    }
}
