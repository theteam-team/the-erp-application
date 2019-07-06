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
        ///Customer 
        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int AddCustomer(Customer customer, byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr getCustomerById(string id,  byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int AddOpportunity(Opportunity opportunity, byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int AddOpportunitie_detail(string opportunity_id, string[] product_id , int numOfProducts, byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int AddCustomerAddress(Address address, byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int AddOpportunityProduct(OpportunityProduct product, byte[] error, ConnectionString connection);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetAllCustomers(out IntPtr customer, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetAllOpportunities(out IntPtr opportunity, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//CRM//CRM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetAllOpportunityProducts(string id, out IntPtr product, byte[] error, ConnectionString connectionString);
    }
}
