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
    /// <summary>
    /// the Wrapper for the Crm c++ Code
    /// </summary>
    class Crm_Wrapper
    {
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddCustomer(Customer customer, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddOpportunity(Opportunity opportunities, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddEmployee(Employee employee, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddRole(Roles roles, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int AddOpportunitie_detail(string opportunity_id, string[] product_id , int numOfProducts, byte[] error);
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void getCustomerById(string id, out IntPtr customer, out int status, byte[] error);

    }
}
