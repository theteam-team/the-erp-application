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
    public class AOrder
    {
        [Required]
        public string id;
        public string requiredDate;
        public string completedDate;
        public string orderStatus;
        public string paymentID;
        public double totalPrice;
    }
}
