using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class OpportunityProduct
    {
        public string opportunityID;
        public string productID;
        public uint units;
    }
}
