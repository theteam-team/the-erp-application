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
    public class Order
    {
       [Required]
        public string Order_ID;
        public string Order_Required_Date;
        public string Order_Completed_Date;
        public string Payment_Payment_ID;
        public string Shipment_Shipment_ID;
        public string Opportunities_Opportunity_ID;
       
    }
}
