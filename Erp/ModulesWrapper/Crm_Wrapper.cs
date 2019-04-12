using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ViewModels;
using Erp.ViewModels.CRN_Tabels;

namespace Erp.ModulesWrappers
{
    /// <summary>
    /// the Wrapper for the Crm c++ Code
    /// </summary>
    class Crm_Wrapper
    {
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddNumbers(int x, int y);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MultiplyNumbers(int x, int y);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddCustomer(Customer customer, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddOpportunity(Opportunities opportunities, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddEmployee(Crm_employee opportunities, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddRole(Crm_roles roles, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddOpportunitie_detail(string opportunity_id, string product_id , byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void getCustomerById(string id, out IntPtr customer, out IntPtr status, byte[] error);

    }
}
