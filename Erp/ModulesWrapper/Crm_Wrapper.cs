using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace Erp.ModulesWrappers
{
    /// <summary>
    /// the Wrapper for the Crm c++ Code
    /// </summary>
    class Crm_Wrapper
    {
        [DllImport("Modules//CRM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddNumbers(int x, int y);
        [DllImport("Modules//CRM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MultiplyNumbers(int x, int y);
        
    }
}
