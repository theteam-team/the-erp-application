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
        public string id;
        public uint incoming;
        public uint outgoing;
        public string requiredDate;
        public string completedDate;
        public string orderStatus;
        public double totalPrice;
        public string customerID;
        public string supplierID;
        public string paymentID;
        public string shipmentID;
    }
}
