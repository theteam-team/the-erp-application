using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class ProductInOrder
    {
	   public string orderID;	
       public string productID;
       public string inventoryID;
       public uint unitsOrdered;
       public uint unitsDone;
    }
}
