using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class ConnectionString
    {
       public string SERVER;
       public string USER;
       public string PORT;
       public string PASSWORD;
       public string DATABASE;
    }
}
