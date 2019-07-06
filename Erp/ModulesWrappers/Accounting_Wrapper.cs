using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Erp.ViewModels;

namespace Erp.ModulesWrappers
{
    public class Accounting_Wrapper
    {

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getProfit(out IntPtr Product, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getInvoice(out IntPtr Invoice, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getCustomerById(string id,  out IntPtr CustomerPtr,  byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getCustomerOrders(string id, out IntPtr OrderPtr, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getOrderProducts(string id, out IntPtr OrderProductPtr, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getCustomerAccount(string id, out IntPtr AccountPtr, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int reporting(out IntPtr OutPtr, byte[] error, ConnectionString connection);

        [DllImport("Modules//Accounting//Accounting.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addInvoice(out IntPtr AnInvoicePtr, byte[] error, ConnectionString connection);
    }
}
