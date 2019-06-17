using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ViewModels;

using Erp.Models;
namespace Erp.ModulesWrappers
{
    public class Accounting_Wrapper
    {

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getProfit(out IntPtr Product, byte[] error);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getInvoice(out IntPtr Invoice, byte[] error);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getCustomerById(string id,  out IntPtr CustomerPtr,  byte[] error);
    }
}
